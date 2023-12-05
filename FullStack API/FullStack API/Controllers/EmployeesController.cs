using FullStack_API.Data;
using FullStack_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FullStack_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly FullStackDbContext _fullStackDbContext;
        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }

        [HttpPost("/api/Auth/getallusers")] // get all users
        
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_fullStackDbContext == null)
            {
                return NotFound();
            }
            return await _fullStackDbContext.Employees.ToListAsync();
        }


        [HttpPost]  // create a new employee
        public async Task<ActionResult<Employee>> PostEmployee(Employee emp)
        {
            emp.id = Guid.NewGuid();
            await _fullStackDbContext.Employees.AddAsync(emp);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(emp);
        }


        [HttpPost]
        [Route("{id:Guid}")] //get employee by ID
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDbContext.Employees.FirstOrDefaultAsync(x => x.id == id);

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        [Route("{id:Guid}/update")] // update an employee
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _fullStackDbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Contact = updateEmployeeRequest.Contact;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            await _fullStackDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPost]
        [Route("{id:Guid}/delete")] //delete an employee

        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
           var employee = await _fullStackDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _fullStackDbContext.Employees.Remove(employee);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(employee);
        }


       

    }

}
