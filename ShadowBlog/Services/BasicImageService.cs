using Microsoft.AspNetCore.Http;
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
        public async Task<byte[]> EncodeImageAsync(IFormFile file)
        {
            if (file is null)
            {
                return null;
            }

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }

        public async Task<byte[]> EncodeImageAsync(string fileName)
        {
            var file = $"{Directory.GetCurrentDirectory()}/wwwroot/img/{fileName}";
            return await File.ReadAllBytesAsync(file);
        }

        public string DecodeImage(byte[] data, string type)
        {
            if (data is null || type is null) return null;
            return $"data:image/{type};base64,{Convert.ToBase64String(data)}";
        }

        public string ContentType(IFormFile file)
        {
            return file?.ContentType;
        }

        public int Size(IFormFile file)
        {
            return Convert.ToInt32(file?.Length);
        }
    }
}
