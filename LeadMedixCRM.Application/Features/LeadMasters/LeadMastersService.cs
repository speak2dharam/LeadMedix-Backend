using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Application.Common.Interfaces.Services;
using LeadMedixCRM.Application.Exceptions;
using LeadMedixCRM.Application.Features.LeadMasters.DTOs;
using LeadMedixCRM.Domain.Common;
using LeadMedixCRM.Domain.Entities.Masters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Features.LeadMasters
{
    public class LeadMastersService : ILeadMastersService
    {
        private readonly IServiceProvider _sp;
        private readonly ICurrentUserService _currentUser;

        public LeadMastersService(IServiceProvider sp, ICurrentUserService currentUser)
        {
            _sp = sp;
            _currentUser = currentUser;
        }

        private static readonly Dictionary<string, Type> MasterMap = new(StringComparer.OrdinalIgnoreCase)
        {
            ["lead-status"] = typeof(LeadStatusMaster),
            ["requirement-type"] = typeof(LeadRequirementTypeMaster),
            ["requirement-status"] = typeof(LeadRequirementStatusMaster),
            ["hospital-review-status"] = typeof(HospitalReviewStatusMaster),
            ["quotation-status"] = typeof(QuotationStatusMaster),
            ["vil-status"] = typeof(VILStatusMaster),
            ["lead-discard-reason"] = typeof(LeadDiscardReasonMaster),
            ["lead-close-reason"] = typeof(LeadCloseReasonMaster),
        };

        public async Task<List<MasterDto>> GetAsync(string masterKey, bool activeOnly = true)
        {
            var entityType = ResolveMaster(masterKey);

            var method = typeof(LeadMastersService).GetMethod(nameof(GetListInternalAsync),
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

            var generic = method.MakeGenericMethod(entityType);
            var task = (Task<List<MasterDto>>)generic.Invoke(this, new object[] { activeOnly })!;
            return await task;
        }

        public async Task<MasterDto> CreateAsync(string masterKey, UpsertMasterRequest request)
        {
            var entityType = ResolveMaster(masterKey);

            var method = typeof(LeadMastersService).GetMethod(nameof(CreateInternalAsync),
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

            var generic = method.MakeGenericMethod(entityType);
            var task = (Task<MasterDto>)generic.Invoke(this, new object[] { request })!;
            return await task;
        }

        public async Task<MasterDto> UpdateAsync(string masterKey, int id, UpsertMasterRequest request)
        {
            var entityType = ResolveMaster(masterKey);

            var method = typeof(LeadMastersService).GetMethod(nameof(UpdateInternalAsync),
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

            var generic = method.MakeGenericMethod(entityType);
            var task = (Task<MasterDto>)generic.Invoke(this, new object[] { id, request })!;
            return await task;
        }

        public async Task<bool> DeleteAsync(string masterKey, int id)
        {
            var entityType = ResolveMaster(masterKey);

            var method = typeof(LeadMastersService).GetMethod(nameof(DeleteInternalAsync),
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

            var generic = method.MakeGenericMethod(entityType);
            var task = (Task<bool>)generic.Invoke(this, new object[] { id })!;
            return await task;
        }

        private Type ResolveMaster(string masterKey)
        {
            if (!MasterMap.TryGetValue(masterKey, out var entityType))
                throw new ValidationException($"Invalid master key: {masterKey}");

            return entityType;
        }

        private IMasterRepository<T> Repo<T>() where T : BaseEntity
            => _sp.GetRequiredService<IMasterRepository<T>>();

        private async Task<List<MasterDto>> GetListInternalAsync<T>(bool activeOnly) where T : BaseEntity
        {
            var repo = Repo<T>();
            var list = await repo.GetListAsync(activeOnly);
            return list.Select(MapToDto).ToList();
        }

        private async Task<MasterDto> CreateInternalAsync<T>(UpsertMasterRequest request) where T : BaseEntity, new()
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("Name is required.");
            if (string.IsNullOrWhiteSpace(request.Code))
                throw new ValidationException("Code is required.");

            var repo = Repo<T>();

            var dup = await repo.GetByCodeAsync(request.Code.Trim());
            if (dup != null)
                throw new ValidationException($"Code already exists: {request.Code}");

            var entity = new T();
            SetProp(entity, "Name", request.Name.Trim());
            SetProp(entity, "Code", request.Code.Trim());
            SetProp(entity, "Description", request.Description);
            SetProp(entity, "IsActive", request.IsActive);
            SetProp(entity, "SortOrder", request.SortOrder);

            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = _currentUser.UserId;

            await repo.AddAsync(entity);
            return MapToDto(entity);
        }

        private async Task<MasterDto> UpdateInternalAsync<T>(int id, UpsertMasterRequest request) where T : BaseEntity
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("Name is required.");
            if (string.IsNullOrWhiteSpace(request.Code))
                throw new ValidationException("Code is required.");

            var repo = Repo<T>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"Master record not found. Id: {id}");

            var currentCode = GetProp<string>(entity, "Code") ?? "";
            if (!string.Equals(currentCode, request.Code.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                var dup = await repo.GetByCodeAsync(request.Code.Trim());
                if (dup != null && GetProp<int>(dup, "Id") != id)
                    throw new ValidationException($"Code already exists: {request.Code}");
            }

            SetProp(entity, "Name", request.Name.Trim());
            SetProp(entity, "Code", request.Code.Trim());
            SetProp(entity, "Description", request.Description);
            SetProp(entity, "IsActive", request.IsActive);
            SetProp(entity, "SortOrder", request.SortOrder);

            entity.UpdatedAt = DateTime.UtcNow;
            //entity.UpdatedBy = _currentUser.UserId;

            await repo.UpdateAsync(entity);
            return MapToDto(entity);
        }

        private async Task<bool> DeleteInternalAsync<T>(int id) where T : BaseEntity
        {
            var repo = Repo<T>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"Master record not found. Id: {id}");

            entity.UpdatedAt = DateTime.UtcNow;
            //entity.UpdatedBy = _currentUser.UserId;

            await repo.SoftDeleteAsync(entity);
            return true;
        }

        private static MasterDto MapToDto<T>(T entity) where T : BaseEntity
        {
            return new MasterDto
            {
                Id = GetProp<int>(entity, "Id"),
                Name = GetProp<string>(entity, "Name") ?? "",
                Code = GetProp<string>(entity, "Code") ?? "",
                Description = GetProp<string?>(entity, "Description"),
                IsActive = GetProp<bool>(entity, "IsActive"),
                SortOrder = GetProp<int>(entity, "SortOrder")
            };
        }

        private static void SetProp<TObj>(TObj obj, string propName, object? value)
        {
            var prop = typeof(TObj).GetProperty(propName);
            if (prop == null) return;
            prop.SetValue(obj, value);
        }

        private static TVal? GetProp<TVal>(object obj, string propName)
        {
            var prop = obj.GetType().GetProperty(propName);
            if (prop == null) return default;
            var val = prop.GetValue(obj);
            if (val == null) return default;
            return (TVal)val;
        }
    }
}
