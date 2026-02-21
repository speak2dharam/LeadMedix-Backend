using LeadMedixCRM.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadMedixCRM.Infrastructure.Files
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;

        public LocalFileStorage(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<(string relativePath, string fileName, string contentType, long size)> SaveAsync(
            Stream stream,
            string originalFileName,
            string contentType,
            string folderRelativeToWwwRoot)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var safeName = Path.GetFileName(originalFileName);
            var uniqueName = $"{Guid.NewGuid():N}_{safeName}";

            var folder = Path.Combine(_env.WebRootPath, folderRelativeToWwwRoot.Replace("/", Path.DirectorySeparatorChar.ToString()));
            Directory.CreateDirectory(folder);

            var fullPath = Path.Combine(folder, uniqueName);

            await using var fs = new FileStream(fullPath, FileMode.Create);
            await stream.CopyToAsync(fs);

            var rel = "/" + $"{folderRelativeToWwwRoot.TrimEnd('/')}/{uniqueName}".Replace("\\", "/");

            var size = new FileInfo(fullPath).Length;
            return (rel, uniqueName, contentType, size);
        }
    }
}
