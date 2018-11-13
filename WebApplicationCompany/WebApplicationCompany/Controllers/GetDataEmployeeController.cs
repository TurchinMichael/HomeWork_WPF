using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationCompany.Models;

namespace WebApplicationCompany.Controllers
{
    public class GetDataEmployeeController : ApiController
    {
        GetEmployee employeeData = new GetEmployee();

        [Route("getlist/{id}")]
        public Employee Get (int id)
        {
            return employeeData.GetEmployeeId(id);
        }

        
        [Route("addemployee")]
        public HttpResponseMessage Post([FromBody]Employee value)
        {
            if (employeeData.AddEmployee(value))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}