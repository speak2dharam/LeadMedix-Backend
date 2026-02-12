using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateAsync(CreateUserDto dto, int createdBy);
        //Task<List<UserResponseDto>> GetAllAsync();
        Task<PaginatedResponse<UserResponseDto>> GetPagedAsync(PaginationRequest request);
        Task<UserResponseDto?> GetByIdAsync(int id);
        Task UpdateAsync(int id, UpdateUserDto dto);
        Task DeleteAsync(int id);
    }
}
