using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHRToolAPI.Models;
using SimpleHRToolAPI.Models.EmployeeModel;
using SimpleHRToolAPI.Models.LeaveRequestModels;

namespace SimpleHRToolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly SimpleHrtoolContext _context;

        public LeaveRequestsController(SimpleHrtoolContext context)
        {
            _context = context;
        }

        // GET: api/LeaveRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveRequestDTO>>> GetLeaveRequests()
        {
            //Automapper Configuration
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<LeaveRequest, LeaveRequestDTO>());
            var mapper = configuration.CreateMapper();

            //Select All Leave Requests from the database
            var leaveRequests = await _context.LeaveRequests.Include(e => e.EmployeeNavigation).
                                Select(e => new LeaveRequest { 
                                    Id = e.Id,
                                    Employee = e.Employee,
                                    AbsenceReason = e.AbsenceReason,
                                    StartDate = e.StartDate,
                                    EndDate = e.EndDate,
                                    Status = e.Status,
                                    EmployeeFullName = e.EmployeeNavigation != null ? e.EmployeeNavigation.FullName : null

                                })
                                .ToListAsync();

            //Create List and then automap it to leaveRequestDTO model
            List<LeaveRequestDTO> leaveRequestDTO = new List<LeaveRequestDTO>();
            leaveRequestDTO = mapper.Map<List<LeaveRequest>, List<LeaveRequestDTO>>(leaveRequests);

            return leaveRequestDTO;
        }

        [HttpGet("NewLeaveRequests")]
        public async Task<ActionResult<IEnumerable<LeaveRequestDTO>>> GetNewLeaveRequests()
        {
            //Automapper Configuration
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<LeaveRequest, LeaveRequestDTO>());
            var mapper = configuration.CreateMapper();

            //Select All Leave Requests from the database
            var leaveRequests = await _context.LeaveRequests.Include(e => e.EmployeeNavigation).
                                Select(e => new LeaveRequest
                                {
                                    Id = e.Id,
                                    Employee = e.Employee,
                                    AbsenceReason = e.AbsenceReason,
                                    StartDate = e.StartDate,
                                    EndDate = e.EndDate,
                                    Status = e.Status,
                                    EmployeeFullName = e.EmployeeNavigation != null ? e.EmployeeNavigation.FullName : null

                                })
                                .Where(e => e.Status == "New")
                                .ToListAsync();

            //Create List and then automap it to leaveRequestDTO model
            List<LeaveRequestDTO> leaveRequestDTO = new List<LeaveRequestDTO>();
            leaveRequestDTO = mapper.Map<List<LeaveRequest>, List<LeaveRequestDTO>>(leaveRequests);

            return leaveRequestDTO;
        }


        [HttpGet("RejectedLeaveRequests")]
        public async Task<ActionResult<IEnumerable<LeaveRequestDTO>>> GetRejectedLeaveRequests()
        {
            //Automapper Configuration
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<LeaveRequest, LeaveRequestDTO>());
            var mapper = configuration.CreateMapper();

            //Select All Leave Requests from the database
            var leaveRequests = await _context.LeaveRequests.Include(e => e.EmployeeNavigation).
                                Select(e => new LeaveRequest
                                {
                                    Id = e.Id,
                                    Employee = e.Employee,
                                    AbsenceReason = e.AbsenceReason,
                                    StartDate = e.StartDate,
                                    EndDate = e.EndDate,
                                    Status = e.Status,
                                    EmployeeFullName = e.EmployeeNavigation != null ? e.EmployeeNavigation.FullName : null

                                })
                                .Where(e => e.Status == "Rejected")
                                .ToListAsync();

            //Create List and then automap it to leaveRequestDTO model
            List<LeaveRequestDTO> leaveRequestDTO = new List<LeaveRequestDTO>();
            leaveRequestDTO = mapper.Map<List<LeaveRequest>, List<LeaveRequestDTO>>(leaveRequests);

            return leaveRequestDTO;
        }


        // GET: api/LeaveRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LeaveRequestDTO>>> GetLeaveRequest(int id)
        {
            //Automapper Configuration
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<LeaveRequest, LeaveRequestDTO>());
            var mapper = configuration.CreateMapper();

            //Select All Leave Requests from the database
            var leaveRequest = await _context.LeaveRequests.Include(e => e.EmployeeNavigation).
                                Select(e => new LeaveRequest
                                {
                                    Id = e.Id,
                                    Employee = e.Employee,
                                    AbsenceReason = e.AbsenceReason,
                                    StartDate = e.StartDate,
                                    EndDate = e.EndDate,
                                    Status = e.Status,
                                    EmployeeFullName = e.EmployeeNavigation != null ? e.EmployeeNavigation.FullName : null

                                })
                                .Where(e => e.Id == id)
                                .ToListAsync();

            if (leaveRequest == null)
            {
                return NotFound();
            }

            //Create List and then automap it to leaveRequestDTO model
            List<LeaveRequestDTO> leaveRequestDTO = new List<LeaveRequestDTO>();
            leaveRequestDTO = mapper.Map<List<LeaveRequest>, List<LeaveRequestDTO>>(leaveRequest);

            return leaveRequestDTO;
        }

        // PUT: api/LeaveRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveRequest(int id, LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(leaveRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveRequestExists(id))
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

        // POST: api/LeaveRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeaveRequest>> PostLeaveRequest(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveRequest", new { id = leaveRequest.Id }, leaveRequest);
        }

        // DELETE: api/LeaveRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            _context.LeaveRequests.Remove(leaveRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaveRequestExists(int id)
        {
            return _context.LeaveRequests.Any(e => e.Id == id);
        }
    }
}
