using LeadMedixCRM.Application.Common.Interfaces;
using LeadMedixCRM.Application.Common.Interfaces.Repositories;
using LeadMedixCRM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            //Leads = new LeadRepository(_db);
            //LeadActivities = new LeadActivityRepository(_db);
        }

        //public ILeadRepository Leads { get; }
        //public ILeadActivityRepository LeadActivities { get; }

        public IRoleRepository Roles { get; }
        public IUserRoleRepository UserRoles { get; }

        public Task<int> SaveAsync(CancellationToken cancellationToken = default)
            => _db.SaveChangesAsync(cancellationToken);
    }
}
