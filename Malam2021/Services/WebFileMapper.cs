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
            WebRootPath = _env.WebRootPath.Replace('/', '\\');
         }

        public string MapPath(string relPath)
        {
            relPath = relPath.Replace('/','\\');
            if (relPath.StartsWith("~\\"))
            {
                return Path.Combine(WebRootPath, relPath.Substring(2));
            }
            else if (relPath.StartsWith("~"))
            {
                return Path.Combine(WebRootPath, relPath.Substring(1));
            }
            else
            {
                return Path.Combine(WebRootPath, relPath.Substring(1));

            }


        }
    }



}
