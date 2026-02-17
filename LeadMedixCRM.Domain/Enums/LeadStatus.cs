using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Domain.Enums
{
    public enum LeadStatus
    {
        New = 0,
        Contacted = 1,
        Qualified = 2,
        QuoteRequested = 3,
        QuoteReceived = 4,
        SharedWithPatient = 5,
        Converted = 6,
        Lost = 7
    }
}
