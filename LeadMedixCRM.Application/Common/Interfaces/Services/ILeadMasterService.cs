using LeadMedixCRM.Application.Features.LeadMasters.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface ILeadMastersService
    {
        Task<List<MasterDto>> GetAsync(string masterKey, bool activeOnly = true);
        Task<MasterDto> CreateAsync(string masterKey, UpsertMasterRequest request);
        Task<MasterDto> UpdateAsync(string masterKey, int id, UpsertMasterRequest request);
        Task<bool> DeleteAsync(string masterKey, int id);
    }
}
