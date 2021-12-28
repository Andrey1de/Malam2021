using Malam2021.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Malam2021.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        // GET: api/<FileController>
        [HttpGet]
        public string  Get()
        {
            return ServerEx.WebRootPath;
        }

        // GET api/<FileController>/5
        [HttpGet("{filePath}")]
        public string Get(string filePath)
        {
            return ServerEx.MapPath(filePath);
        }

        //// POST api/<FileController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<FileController>/5
        [HttpPut("{filePath}")]
        [Produces("application/json")]
        public dynamic Put(string filePath)
        {
            try
            {
                string path = ServerEx.MapPath(filePath);
                string file = System.IO.File.ReadAllText(path);
                dynamic dyn = JsonConvert.DeserializeObject(file);
                return dyn;

            }
            catch (Exception ex)
            {

                return new NotFoundResult();
            }
        }

        //// DELETE api/<FileController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
