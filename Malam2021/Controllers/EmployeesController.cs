using Malam2021.Models;
using Malam2021.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Malam2021.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IDataAccessService dataAccessService;

        public EmployeesController(IDataAccessService _dataAccessService)
        {
            dataAccessService = _dataAccessService;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public List<Employee> Get()
        {
            return dataAccessService.Employees.Values.ToList();
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return dataAccessService.GetEmployee(id);
        }
       

        //// POST api/<EmployeesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<EmployeesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EmployeesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
