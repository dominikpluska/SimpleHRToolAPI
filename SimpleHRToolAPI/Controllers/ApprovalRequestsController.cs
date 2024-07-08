using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHRToolAPI.Models;
using SimpleHRToolAPI.Models.ApprovalRequestModel;

namespace SimpleHRToolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalRequestsController : ControllerBase
    {
        private readonly SimpleHrtoolContext _context;

        public ApprovalRequestsController(SimpleHrtoolContext context)
        {
            _context = context;
        }

        // GET: api/ApprovalRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApprovalRequestDTO>>> GetApprovalRequests()
        {
            var approvalRequests = await  _context.ApprovalRequests.Include(e =>  e.ApproverNavigation).Include(e => e.LeaveRequestNavigation).
                Select(e => new ApprovalRequestDTO 
                { 
                  Id = e.Id, 
                  Approver = e.ApproverNavigation != null ? e.ApproverNavigation.FullName : null, 
                  LeaveRequest = e.LeaveRequestNavigation != null ? e.LeaveRequestNavigation.AbsenceReason : null,
                  Status = e.Status,
                }).ToListAsync();

            return approvalRequests;
        }


        [HttpGet("FilteredApprovalRequests/{status}")]
        public async Task<ActionResult<IEnumerable<ApprovalRequestDTO>>> GetFilteredApprovalRequests(string status)
        {
            var approvalRequests = await _context.ApprovalRequests.Include(e => e.ApproverNavigation).Include(e => e.LeaveRequestNavigation).
                Select(e => new ApprovalRequestDTO
                {
                    Id = e.Id,
                    Approver = e.ApproverNavigation != null ? e.ApproverNavigation.FullName : null,
                    LeaveRequest = e.LeaveRequestNavigation != null ? e.LeaveRequestNavigation.AbsenceReason : null,
                    Status = e.Status,
                }).Where(e => e.Status == status).
                ToListAsync();

            return approvalRequests;
        }

        // GET: api/ApprovalRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ApprovalRequestDTO>>> GetApprovalRequest(int id)
        {
            var approvalRequests = await _context.ApprovalRequests.Include(e => e.ApproverNavigation).Include(e => e.LeaveRequestNavigation).
                Select(e => new ApprovalRequestDTO
                {
                    Id = e.Id,
                    Approver = e.ApproverNavigation != null ? e.ApproverNavigation.FullName : null,
                    LeaveRequest = e.LeaveRequestNavigation != null ? e.LeaveRequestNavigation.AbsenceReason : null,
                    Status = e.Status,
                }).Where(e => e.Id == id).
                ToListAsync();

            return approvalRequests;

        }

        // PUT: api/ApprovalRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApprovalRequest(int id, ApprovalRequest approvalRequest)
        {
            if (id != approvalRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(approvalRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApprovalRequestExists(id))
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

        // POST: api/ApprovalRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApprovalRequest>> PostApprovalRequest(ApprovalRequest approvalRequest)
        {
            _context.ApprovalRequests.Add(approvalRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApprovalRequest", new { id = approvalRequest.Id }, approvalRequest);
        }

        // DELETE: api/ApprovalRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApprovalRequest(int id)
        {
            var approvalRequest = await _context.ApprovalRequests.FindAsync(id);
            if (approvalRequest == null)
            {
                return NotFound();
            }

            _context.ApprovalRequests.Remove(approvalRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApprovalRequestExists(int id)
        {
            return _context.ApprovalRequests.Any(e => e.Id == id);
        }
    }
}
