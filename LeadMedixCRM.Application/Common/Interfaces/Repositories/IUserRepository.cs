using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        //Task<List<User>> GetAllAsync();
        Task<(List<User>, int totalRecords)> GetPagedAsync(int pageNumber, int pageSize);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task UpdateAsync(User user);
    }
}
