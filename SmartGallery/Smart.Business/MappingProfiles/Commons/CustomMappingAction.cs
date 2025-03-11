using AutoMapper;
using Smart.Business.DTOs.Commons;
using Smart.Business.Services.ExternalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.MappingProfiles.Commons
{
    public class CustomMappingAction<TSource, TDestination> : IMappingAction<TSource, TDestination> where TSource : IAuditedentityDTO
    {
        private readonly IFileManagerService _fileManagerService;

        public CustomMappingAction(IFileManagerService fileManagerService)
        {
            _fileManagerService = fileManagerService;
        }

        public void Process(TSource source, TDestination destination, ResolutionContext context)
        {
            if (source.Image != null)
            {
                var uploadedUrl = _fileManagerService.UploadFileAsync(source.Image).Result;
                var property = destination.GetType().GetProperty("ImageUrl");

                if (property != null && property.PropertyType == typeof(string))
                {
                    property.SetValue(destination, uploadedUrl);
                }
            }
        }
    }
}