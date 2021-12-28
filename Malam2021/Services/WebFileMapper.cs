using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Malam2021.Services
{
    public interface IWebFileMapper
    {
        public string WebRootPath { get; }
        public string MapPath(string relPath);
    }
    public class WebFileMapper : IWebFileMapper
    {
        readonly IWebHostEnvironment _env;
        public string WebRootPath { get; private set; }
        public WebFileMapper(IWebHostEnvironment env)
        {
            _env = env;
            WebRootPath = _env.WebRootPath;

         }

        public string MapPath(string relPath)
        {
            string rett;
           
            if (relPath.StartsWith("~\\"))
            {
                rett = Path.Combine(WebRootPath, relPath.Substring(2));
            }
            else if (relPath.StartsWith("~"))
            {
                rett = Path.Combine(WebRootPath, relPath.Substring(1));
            }
            else
            {
                rett = Path.Combine(WebRootPath, relPath);

            }

            Console.WriteLine($"MapPath({relPath}) => {rett}");
            return rett;

        }
    }



}
