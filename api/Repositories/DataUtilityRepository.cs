using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class DataUtilityRepository: IDataUtilityRepository
    {
        private readonly InventoryDbContext _context;

        public DataUtilityRepository(InventoryDbContext context) => _context = context;

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            var records = await _context.Set<T>().ToListAsync();
            return records;
        }

        public async Task<T?> GetById<T>(int id) where T : class
        {
            var entity = await _context.FindAsync<T>();
            return entity;
        }

        public async Task<int> GetCount<T>() where T : class
        {
            var entityCount = await _context.Set<T>().CountAsync();
            return entityCount;
        }
    }
}

// continue here