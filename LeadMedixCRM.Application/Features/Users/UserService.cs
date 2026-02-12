using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Common.Pagination;
using LeadMedixCRM.Application.Exceptions;
using LeadMedixCRM.Application.Features.Users.DTOs;
using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateAsync(CreateUserDto dto, int createdBy)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser == null) 
            {
                var user = new User
                {
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Mobile = dto.Mobile,
                    //PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    PasswordHash = dto.Password,
                    RoleId = dto.RoleId,
                    CreatedBy = createdBy,
                    CreatedAt = DateTime.UtcNow
                };
                await _userRepository.AddAsync(user);
            }
            else
            {
                throw new NotFoundException("Already added");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                //throw new Exception("User not found");
                throw new NotFoundException("User not found");

            user.IsDeleted = true;

            await _userRepository.UpdateAsync(user);
        }

        //public async Task<List<UserResponseDto>> GetAllAsync()
        //{
        //    var users = await _userRepository.GetAllAsync();

        //    return users
        //        .Where(x => !x.IsDeleted)
        //        .Select(u => new UserResponseDto
        //        {
        //            Id = u.Id,
        //            FirstName = u.FirstName,
        //            LastName = u.LastName,
        //            Email = u.Email,
        //            Mobile = u.Mobile,
        //            IsActive = u.IsActive
        //        }).ToList();
        //}
        public async Task<PaginatedResponse<UserResponseDto>> GetPagedAsync(PaginationRequest request)
        {
            var (users, totalRecords) = await _userRepository
                .GetPagedAsync(request.PageNumber, request.PageSize);

            var data = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                Email = u.Email
            }).ToList();

            return new PaginatedResponse<UserResponseDto>
            {
                Data = data,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize)
            };
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null || user.IsDeleted)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                IsActive = user.IsActive
            };
        }

        public async Task UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null || user.IsDeleted)
                throw new Exception("User not found");

            user.FirstName = dto.FirstName;
            user.MiddleName = dto.MiddleName;
            user.LastName = dto.LastName;
            user.Mobile = dto.Mobile;
            user.IsActive = dto.IsActive;
            user.RoleId = dto.RoleId;

            await _userRepository.UpdateAsync(user);
        }
    }
}