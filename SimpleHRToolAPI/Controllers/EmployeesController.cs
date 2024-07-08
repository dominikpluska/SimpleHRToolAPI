using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Elfie.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHRToolAPI.Models;
using SimpleHRToolAPI.Models.EmployeeModel;

namespace SimpleHRToolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly SimpleHrtoolContext _context;

        public EmployeesController(SimpleHrtoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesProper()
        {
            //Automapper Configuration
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>());
            var mapper = configuration.CreateMapper();

            //Select All Users from the database
            var employees = await _context.Employees
                .Include(e => e.PeoplePartnerNavigation)
                .Select(e => new Employee
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Subdivision = e.Subdivision,
                    Position = e.Position,
                    PeoplePartnerFullName = e.PeoplePartnerNavigation != null ? e.PeoplePartnerNavigation.FullName : null,
                    Status = e.Status,

                })
                .ToListAsync();


            //Create List and then automap it to EmployeeDTO model
            List<EmployeeDTO> employeeDTO = new List<EmployeeDTO>();
            employeeDTO = mapper.Map<List<Employee>, List<EmployeeDTO>>(employees);

            return employeeDTO.ToList(); ;

        }

        // GET: api/Employees
        [HttpGet("GetActiveEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetActiveEmployees()
        {
            //Automapper Configuration
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>());
            var mapper = configuration.CreateMapper();

            //Select Active Users from the database
            var employees = await _context.Employees
                .Include(e => e.PeoplePartnerNavigation) 
                .Select(e => new Employee
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Subdivision = e.Subdivision,
                    Position = e.Position,
                    PeoplePartnerFullName = e.PeoplePartnerNavigation != null ? e.PeoplePartnerNavigation.FullName : null,
                    Status = e.Status,

                })
                .Where(e => e.Status == true)
                .ToListAsync();

            //Create List and then automap it to EmployeeDTO model
            List<EmployeeDTO> employeeDTO = new List<EmployeeDTO>();
            employeeDTO = mapper.Map<List<Employee>, List<EmployeeDTO>>(employees);

            return employeeDTO.ToList(); ;
        }


        [HttpGet("GetInactiveEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetInactiveEmployees()
        {
            //Automapper Configuration
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>());
            var mapper = configuration.CreateMapper();

            //Select Inactive Users from the database
            var employees = await _context.Employees
                .Include(e => e.PeoplePartnerNavigation)
                .Select(e => new Employee
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Subdivision = e.Subdivision,
                    Position = e.Position,
                    PeoplePartnerFullName = e.PeoplePartnerNavigation != null ? e.PeoplePartnerNavigation.FullName : null,
                    Status = e.Status,

                })
                .Where(e => e.Status == false)
                .ToListAsync();

            List<EmployeeDTO> employeeDTO = new List<EmployeeDTO>();
            employeeDTO = mapper.Map<List<Employee>, List<EmployeeDTO>>(employees);

            return employeeDTO.ToList(); 

        }

        [HttpGet("GetEmployeeByName/{name}")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeeByName(string name)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>());
            var mapper = configuration.CreateMapper();

            var employees = await _context.Employees.Select(e => new Employee
            {
                Id = e.Id,
                FullName = e.FullName,
                Subdivision = e.Subdivision,
                Position = e.Position,
                PeoplePartnerFullName = e.PeoplePartnerNavigation != null ? e.PeoplePartnerNavigation.FullName : null,
                Status = e.Status,

            }).Where(x => x.FullName.Contains(name)).ToListAsync();

            List<EmployeeDTO> employeeDTO = new List<EmployeeDTO>();
            employeeDTO = mapper.Map<List<Employee>, List<EmployeeDTO>>(employees);

            return employeeDTO.ToList();

        }

        [HttpGet("GetListOfPeoplePartners")]
        public async Task<ActionResult<IEnumerable<string>>> GetListOfPeoplePartners()
        {
            return await _context.Employees.Where(x => x.Subdivision == "HR").Select(x => x.FullName).ToListAsync();
        }



        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
