using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class RecordValidationRepository : IRecordValidationRepository
    {
        private readonly InventoryDbContext _context;

        public RecordValidationRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetPartialRecords<T>() where T : class
        {
            var lambda = BuildNullCheckExpression<T>();
            if (lambda == null)
                return new List<T>();

            return await _context.Set<T>().Where(lambda).ToListAsync();
        }

        public async Task<int> GetTotalPartialRecords<T>() where T : class
        {
            var lambda = BuildNullCheckExpression<T>();
            if (lambda == null)
                return 0;

            return await _context.Set<T>().CountAsync(lambda);
        }

        /// <summary>
        /// Builds a lambda expression like: x => x.Prop1 == null || x.Prop2 == null ...
        /// for all nullable properties on T.
        /// </summary>
        private static Expression<Func<T, bool>>? BuildNullCheckExpression<T>() where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? body = null;

            foreach (var prop in typeof(T).GetProperties())
            {
                // Check for reference type or Nullable<T>
                if (!prop.PropertyType.IsValueType || Nullable.GetUnderlyingType(prop.PropertyType) != null)
                {
                    var propAccess = Expression.Property(parameter, prop);
                    var nullConstant = Expression.Constant(null, prop.PropertyType);
                    var equals = Expression.Equal(propAccess, nullConstant);

                    body = body == null ? equals : Expression.OrElse(body, equals);
                }
            }

            if (body == null)
                return null;

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
