using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Smart.Business.Services.ExternalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.ExternalServices.Abstractions
{
    public class FileManagerService : IFileManagerService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly Cloudinary _cloudinary;

        public FileManagerService(IWebHostEnvironment environment, Cloudinary cloudinary)
        {
            _environment = environment;
            _cloudinary = cloudinary;
        }

        public static bool BeAValidImage(IFormFile file)
        {
            if (file == null) return false;

            var validImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName)?.ToLower();

            return validImageExtensions.Contains(fileExtension);
        }

        public static bool IsValidImageUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            var validImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(new Uri(url).AbsolutePath)?.ToLower();

            return validImageExtensions.Contains(extension);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string cloudUrl = await UploadToCloudAsync(file);

            return cloudUrl;
        }

        public async Task<string> UploadToCloudAsync(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "uploads"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                return uploadResult?.SecureUrl?.ToString();
            }
        }

        public async Task<List<string>> UploadMultipleAsync(List<IFormFile> files)
        {
            if (files == null || !files.Any())
                throw new ArgumentException("Ən azı bir fayl daxil etməyiniz mütləqdir!");

            var fileNames = new List<string>();

            foreach (var file in files)
            {
                if (file.Length == 0)
                    throw new ArgumentException("Faylın uzunluğu sıfırdır.");

                if (!BeAValidImage(file))
                    throw new Exception("Yalnız şəkil faylları (jpeg, png, gif) qəbul edilir!");

                var fileName = Guid.NewGuid().ToString() + "_" +
                               Path.GetFileNameWithoutExtension(file.FileName) +
                               Path.GetExtension(file.FileName);

                var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var filePath = Path.Combine(uploadsPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                fileNames.Add(fileName);
            }

            return fileNames;
        }

        public static bool IsFromCloudService(string url, string cloudBaseUrl = "https://yourcloudservice.com/")
        {
            return url.StartsWith(cloudBaseUrl);
        }
    }
}
