using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Security
{
    public static class Policies
    {
        public const string MasterDataView = "MasterData.View";
        public const string MasterDataEdit = "MasterData.Edit";
        public const string MasterDataApprove = "MasterData.Approve";

        // Leads
        public const string LeadCreate = "Lead.Create";
        public const string LeadViewAll = "Lead.ViewAll";
        public const string LeadViewAssigned = "Lead.ViewAssigned";
        public const string LeadEdit = "Lead.Edit";
        public const string LeadAssign = "Lead.Assign";
        public const string LeadUpdateStatus = "Lead.UpdateStatus";
        public const string LeadDiscard = "Lead.Discard";
        public const string LeadRestore = "Lead.Restore";
        public const string LeadClose = "Lead.Close";
        public const string LeadReopen = "Lead.Reopen";
        public const string LeadMerge = "Lead.Merge";
    }
}
