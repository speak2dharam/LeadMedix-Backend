using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IRoleRepository Roles { get; }
        IUserRoleRepository UserRoles { get; }

        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
