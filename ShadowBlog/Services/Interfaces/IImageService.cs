using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Services.Interfaces
{
    public interface IImageService //This interface will get used for the rest of the cohort.
    /*A set of methods and properties that I have guaranteed access to. In a method it specifies three parameters
    */
    {
       Task<byte[]> EncodeImageAsync(IFormFile file);

       Task<byte[]> EncodeImageAsync(string fileName);

       string DecodeImage(byte[] data, string type);

       string ContentType(IFormFile file);

        bool ValidImage(IFormFile file);
    }
}