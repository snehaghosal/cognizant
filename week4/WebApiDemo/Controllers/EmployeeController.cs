using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Filters;
using WebApiDemo.Models;
namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,POC")]
    [ServiceFilter(typeof(CustomAuthFilter))]
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _employees = GetStandardEmployeeList();

        private static List<Employee> GetStandardEmployeeList()
        {
            return new List<Employee>
            {
                new Employee { Id = 1, Name = "John", Salary = 50000, Permanent = true, DateOfBirth = new DateTime(1990, 1, 1),
                    Department = new Department { DeptId = 1, DeptName = "HR" }, Skills = new List<Skill> { new Skill { Id = 1, SkillName = "C#" } } }
            };
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<List<Employee>> GetStandard()
        {
            //throw new Exception("Test exception");
            return Ok(_employees);
        }

        [HttpPut("{id}")]
        public ActionResult<Employee> UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            if (id <= 0)
                return BadRequest("Invalid employee id");

            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return BadRequest("Invalid employee id");

            emp.Name = updatedEmployee.Name;
            emp.Salary = updatedEmployee.Salary;
            emp.Permanent = updatedEmployee.Permanent;
            emp.Department = updatedEmployee.Department;
            emp.Skills = updatedEmployee.Skills;
            emp.DateOfBirth = updatedEmployee.DateOfBirth;

            return Ok(emp);
        }
    }
}