using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface ILookupRepository
    {
        Task<Dictionary<int, string>> GetHospitalNamesByIdsAsync(List<int> ids);
        Task<string?> GetHospitalNameByIdAsync(int id);
    }
}
