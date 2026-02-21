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
    }
}
