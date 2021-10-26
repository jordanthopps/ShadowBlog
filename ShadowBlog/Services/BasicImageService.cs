using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShadowBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Services
{
    public class BasicImageService /*Class name*/ : IImageService /*Interface*/
    {
        private readonly IConfiguration _configuration;

        public BasicImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<byte[]> EncodeImageAsync(IFormFile file)
        {
            if (file is null)
            {
                return null;
            }

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        } //encodes an image that a user manually selects/uploads

        public async Task<byte[]> EncodeImageAsync(string fileName) 
        {
            var file = $"{Directory.GetCurrentDirectory()}/wwwroot/img/{fileName}";
            return await File.ReadAllBytesAsync(file);
        } //encodes a file in the application (like wwwroot default images)

        public string ContentType(IFormFile file) //file extension.
        {
            return file?.ContentType; //if available, return the extension of the file.
        }

        public string DecodeImage(byte[] data, string type)
        {
            if (data is null || type is null) return null;
            return $"data:image/{type};base64,{Convert.ToBase64String(data)}";
        }

        public bool ValidType(IFormFile file)
        {
            var fileContentType = ContentType(file).Split("/")[1]; //TODO: Fix this for Blogs Edit

            var acceptableExtensions = _configuration["AppImages:AllowedExtensions"];
            var extList = acceptableExtensions.Split(',').ToList();
            var position = extList.IndexOf(fileContentType);

            return position >= 0;
        }

        public bool ValidSize(IFormFile file) 
        {
            const int maxFileSize = 2 * 1024 * 1024;
            return Size(file) < maxFileSize;
        }

        public bool ValidImage(IFormFile file)
        {
            return (ValidType(file) && ValidSize(file));
        }

        private int Size(IFormFile file)
        {
            return Convert.ToInt32(file?.Length);
        }
    }
}
