using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Smart.Business.Services.ExternalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.ExternalServices.Abstractions
{
    public class FileManagerService : IFileManagerService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly Cloudinary _cloudinary;
        public FileManagerService(IWebHostEnvironment environment, Cloudinary cloudinary = null)
        {
            _environment = environment;
            _cloudinary = cloudinary;
        }

        public bool BeAValidPdf(IFormFile file)
        {
            return file != null && file.ContentType.Equals("application/pdf") && 1024 * 1024 * 5 >= file.Length;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string cloudUrl = await UploadToCloudAsync(file);

            return cloudUrl;
        }

        public async Task<string> UploadToCloudAsync(IFormFile file)
        {
            using(var stream = file.OpenReadStream())
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
        public async Task<string> UploadLocalAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Fayl daxil etməyiniz mütləqdir!");

            if (!BeAValidPdf(file))
                throw new Exception("Fayl formatı düzgün qeyd olunmayıb. Daxil olunan fayl pdf formatında və ən çox 1 MB olmalıdır!");

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

            return fileName;
        }
    }
}
