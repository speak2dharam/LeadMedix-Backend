using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Pagination
{
    public static class PaginationHelper
    {
        public static PaginatedResponse<T> Create<T>(List<T> data, int totalRecords, int pageNumber, int pageSize)
        {
            return new PaginatedResponse<T>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };
        }
    }
}
