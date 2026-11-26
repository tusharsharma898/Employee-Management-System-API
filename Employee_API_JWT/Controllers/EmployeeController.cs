using Employee_API_JWT.Identity;
using Employee_API_JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_API_JWT.Controllers
{
    [Route("api/employee")]
    [ApiController]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employeeInDb = _context.Employees.ToList();
            return Ok(employeeInDb);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest();
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            if (employee == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest();
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteEmployee(int id)
        {
            var employeeInDb = _context.Employees.Find(id);
            if (employeeInDb == null) return NotFound();
            _context.Employees.Remove(employeeInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
