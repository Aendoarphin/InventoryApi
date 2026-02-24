using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Interfaces
{
    public interface IDataUtilityRepository
    {
        Task<IEnumerable<T>> GetAll<T>() where T : class;
        Task<T?> GetById<T>(int id) where T : class;
        Task<int> GetCount<T>() where T : class;
        Task<IEnumerable<T>> Search<T>(string keyword) where T : class;
        Task<IEnumerable<T>> GetPartial<T>() where T : class;
        Task<int> GetPartialCount<T>() where T : class;
        Task<RecordMetricDto> GetMetrics<T>() where T : class;
        Task<T?> Create<T>(T entity) where T : class;
        Task<T?> Update<T>(int id, T entity) where T : class;
        Task<T?> Delete<T>(int id) where T : class;
    }
}