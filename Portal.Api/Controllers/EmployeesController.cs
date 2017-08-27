using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models;

namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        DAL dal = new DAL();
        // GET api/values
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return dal.GetAllEmployees();
        }

        [HttpGet("{id}")]
        public IEnumerable<Employee> Get(string Id)
        {
            return dal.GetEmployee(Id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Employee employee)
        {
            dal.CreateEmployee(employee);
        }
        
        
        [HttpDelete]
        public bool Flush()
        {
            return dal.FlushEmployeeCollection();

        }
    }
}
