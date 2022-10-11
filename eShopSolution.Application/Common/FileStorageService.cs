using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace eShopSolution.Application.Common
{
    public class FileStorageService : IStorageService
    {
        private readonly string _imageContentFolder;
        private const string IMAGE_CONTENT_FOLDER_NAME = "Thumbnail";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _imageContentFolder = Path.Combine(Directory.GetCurrentDirectory(), IMAGE_CONTENT_FOLDER_NAME);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_imageContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{IMAGE_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_imageContentFolder, fileName);
            using (var output = new FileStream(filePath, FileMode.Create))
            {
                await mediaBinaryStream.CopyToAsync(output);
            }
        }
    }
}
