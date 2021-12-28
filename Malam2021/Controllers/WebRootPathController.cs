using Malam2021.Models;
using Malam2021.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Malam2021.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebRootPathController : ControllerBase
    {
        private readonly IWebHostEnvironment env;

        public WebRootPathController(IWebHostEnvironment _env)
        {
            env = _env;
        }
        // GET: api/<WebRootPathController>
        
        [HttpGet]
        public string Get(string file)
        {
            file = file ?? "";
            if (file.StartsWith("~/") || file.StartsWith("~\\"))
            {
                file = file.Substring(2);
            } else if (file.StartsWith("/") || file.StartsWith("\\"))
            {
                file = file.Substring(1);
            }

            var path = env.WebRootPath;

            if(path.Contains("\\")) {
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

        //// GET api/<WebRootPathController>/5
        //[HttpGet("{id}")]
        //public Department Get(int id)
        //{
        //    return dataAccessService.GetDepartment(id);
        //}

        //// POST api/<WebRootPathController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<WebRootPathController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<WebRootPathController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
