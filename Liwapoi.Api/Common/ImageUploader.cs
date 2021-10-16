using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Liwapoi.Api.Common
{
    public class ImageUploader
    {
        public async Task<string> Upload(IFormFile file)
        {
            var folderName = Path.Combine("StaticFiles", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                if (fileName != null)
                {
                    var originalFileName = fileName.Trim('"');
                    var fileExtension = originalFileName.Split('.').LastOrDefault();
                    var uniqFileName = GenerateFileName(fileExtension);
                    var fullPath = Path.Combine(pathToSave, uniqFileName);
                    //using var stream = new FileStream(fullPath, FileMode.Create);
                    //await file.CopyToAsync(stream);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return uniqFileName;
                }
            }
            return string.Empty;
        }

        private string GenerateFileName(string extension)
        {
            var guid = Guid.NewGuid();
            var fileName = $"{guid}.{extension}";
            return fileName;
        }
    }
}
