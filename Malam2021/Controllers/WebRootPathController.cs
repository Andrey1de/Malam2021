using Malam2021.Models;
using Malam2021.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Malam2021.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebRootPathCController : ControllerBase
    {
        private readonly IWebHostEnvironment env;

        public WebRootPathCController(IWebHostEnvironment _env)
        {
            env = _env;
        }
        // GET: api/<WebRootPathCController>
        [HttpGet]
        public string Get()
        {
            var ret = env.WebRootPath;
            return ret;
        }

        //// GET api/<WebRootPathCController>/5
        //[HttpGet("{id}")]
        //public Department Get(int id)
        //{
        //    return dataAccessService.GetDepartment(id);
        //}

        //// POST api/<WebRootPathCController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<WebRootPathCController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<WebRootPathCController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
