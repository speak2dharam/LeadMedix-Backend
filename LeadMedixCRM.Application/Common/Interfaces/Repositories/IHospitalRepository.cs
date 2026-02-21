using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IHospitalRepository
    {
        //Task<List<Hospital>> GetAllAsync(string? search);
        Task<(List<Hospital> Items, int TotalRecords)> GetPagedAsync(PaginationRequest request);
        Task<Hospital?> GetByIdAsync(int id);
        Task<int> AddAsync(Hospital entity);
        Task<bool> UpdateAsync(Hospital entity);
        Task<bool> SoftDeleteAsync(int id);
    }
}
