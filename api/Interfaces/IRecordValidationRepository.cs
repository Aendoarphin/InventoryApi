using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Interfaces
{
    public interface IRecordValidationRepository
    {
        Task<List<T>> GetPartialRecords<T>() where T : class;
        Task<int> GetTotalPartialRecords<T>() where T : class;
    }
}