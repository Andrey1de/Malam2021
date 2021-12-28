using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malam2021.Extensions
{
    public static class ServerEx
    {
        public static string WebRootPath => 
            Startup.Environment.WebRootPath;


        public static string MapPath(string file)
        {

            file = file ?? "";
            if (file.StartsWith("~/") || file.StartsWith("~\\"))
            {
                file = file.Substring(2);
            }
            else if (file.StartsWith("/") || file.StartsWith("\\"))
            {
                file = file.Substring(1);
            }
            // Dont't forget !!!!;
            var path = WebRootPath;

            if (path.Contains("\\"))
            {
                file = file.Replace("/", "\\");
                path += "\\" + file;
            }
            else if (path.Contains("/"))
            {
                file = file.Replace("\\", "/");
                path += "/" + file;
            }

            return path;

        }
    }
}
