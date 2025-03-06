using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.ExternalServices.Interfaces
{
    public interface IFileManagerService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
