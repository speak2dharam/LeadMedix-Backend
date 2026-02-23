using LeadMedixCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Repositories
{
    public interface IHospitalAccreditationRepository
    {
        Task<List<HospitalAccreditation>> GetByHospitalIdAsync(int hospitalId);
        Task<HospitalAccreditation?> GetByHospitalAndAccreditationAsync(int hospitalId, int accreditationId);

        Task AddAsync(HospitalAccreditation entity);
        void Update(HospitalAccreditation entity);
    }
}
