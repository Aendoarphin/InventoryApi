using Api.Data;
using Api.Dtos;
using Api.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class DataUtilityRepository: IDataUtilityRepository
    {
        private readonly InventoryDbContext _context;

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

            if (body == null) return null;

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public DataUtilityRepository(InventoryDbContext context) => _context = context;

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            var records = await _context.Set<T>().ToListAsync();
            return records;
        }

        public async Task<T?> GetById<T>(int id) where T : class
        {
            var entity = await _context.FindAsync<T>(id);
            return entity;
        }

        public async Task<int> GetCount<T>() where T : class
        {
            var entityCount = await _context.Set<T>().CountAsync();
            return entityCount;
        }

        public async Task<IEnumerable<T>> GetPartial<T>() where T : class
        {
            var lambda = BuildNullCheckExpression<T>();
            if (lambda == null) return new List<T>();
            return await _context.Set<T>().Where(lambda).ToListAsync();

        }

        public async Task<int> GetPartialCount<T>() where T : class
        {
            var lambda = BuildNullCheckExpression<T>();
            if (lambda == null) return 0;
            return await _context.Set<T>().CountAsync(lambda);
        }

        public async Task<RecordMetricDto> GetMetrics<T>() where T : class
        {
            var partialCount = await GetPartialCount<T>();
            var totalCount = await GetCount<T>();
            return new RecordMetricDto
            {
                Complete = totalCount - partialCount,
                Partial = partialCount
            };
        }

        public async Task<T?> Create<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> Update<T>(int id, T entity) where T : class
        {
            var existing = await _context.FindAsync<T>(id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return existing;
        }

        public async Task<T?> Delete<T>(int id) where T : class
        {
            var record = await _context.FindAsync<T>(id);
            if (record == null) return null;
            _context.Set<T>().Remove(record);
            await _context.SaveChangesAsync();
            return record;
        }
    }
}