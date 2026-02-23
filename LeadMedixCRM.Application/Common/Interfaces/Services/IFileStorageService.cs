using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Application.Common.Interfaces.Services
{
    public interface IFileStorageService
    {
        Task<(string relativePath, string fileName, string contentType, long size)> SaveAsync(
            Stream stream,
            string originalFileName,
            string contentType,
            string folderRelativeToWwwRoot);
    }
}
