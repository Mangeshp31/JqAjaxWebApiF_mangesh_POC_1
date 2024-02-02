using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiCrudCodeFirstDemo.Data;
using WebApiCrudCodeFirstDemo.Models;

namespace WebApiCrudCodeFirstDemo.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeesController : ApiController
    {
        public HttpResponseMessage Get() 
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return Request.CreateResponse(HttpStatusCode.OK,dbContext.Employees.ToList());
            }
        }

        //Get All Employee List
        public HttpResponseMessage Get(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else 
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id + " not found");
                }
            }
        }


        //Add Emp
        public HttpResponseMessage Post(Employee employee)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                if (employee != null)
                {
                    dbContext.Employees.Add(employee);
                    dbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, employee);
                }
                else 
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please provide proper input data to create an employee");
                }
            }
        }


        //Update Emp Details
        public HttpResponseMessage Put(int id, Employee employee)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    emp.FirstName = employee.FirstName;
                    emp.LastName = employee.LastName;
                    emp.Gender = employee.Gender;
                    emp.City = employee.City;
                    emp.Salary = employee.Salary;
                    emp.Department = employee.Department;
                    emp.IsActive = employee.IsActive;
                    dbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id + " not found in database. Update failed.");
                }
            }
        }

        public HttpResponseMessage Patch(int id, [FromBody]string FistName) 
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    emp.FirstName = FistName;
                   
                    dbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id + " not found in database. Update failed.");
                }
            }
        }


        //Delete EMp
        public HttpResponseMessage Delete(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    dbContext.Employees.Remove(emp);
                    dbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id + " not found in database. Delete failed");
                }

            }
        }
    }
}
