using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IRecordValidationRepository
    {
        Task<List<T>> GetPartialRecords<T>() where T : class;
        Task<int> GetTotalPartialRecords<T>() where T : class;
    }
}