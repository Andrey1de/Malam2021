using Malam2021.Models;
using Malam2021.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Malam2021.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDataAccessService dataAccessService;

        public DepartmentsController(IDataAccessService _dataAccessService)
        {
             dataAccessService = _dataAccessService;
        }
        // GET: api/<DepartmentsController>
        [HttpGet]
        public Department[] Get()
        {
            var ret = dataAccessService.Departments.Values.ToArray();
            return ret;
        }

        // GET api/<DepartmentsController>/5
        [HttpGet("{id}")]
        public Department Get(int id)
        {
            return dataAccessService.GetDepartment(id);
        }

        //// POST api/<DepartmentsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<DepartmentsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<DepartmentsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
