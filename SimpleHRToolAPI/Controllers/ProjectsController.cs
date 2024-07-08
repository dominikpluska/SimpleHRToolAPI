using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHRToolAPI.Models;
using SimpleHRToolAPI.Models.ProjectModel;


namespace SimpleHRToolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly SimpleHrtoolContext _context;

        public ProjectsController(SimpleHrtoolContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            //Automapper Configuration
            //var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>());
            //var mapper = configuration.CreateMapper();

            var projects = await _context.Projects.Include(e => e.ProjectManagerNavigation).
                Select(e => 
                    new ProjectDTO
                    {
                        Id = e.Id,
                        ProjectType = e.ProjectType,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        Status = e.Status,
                        ProjectManagerFullName = e.ProjectManagerNavigation != null ? e.ProjectManagerNavigation.FullName : null,
                    }
                ).ToListAsync();

            //List<ProjectDTO> projectDTO = new List<ProjectDTO>();
            //projectDTO = mapper.Map<List<Project>, List<ProjectDTO>>(projects);

            return projects;
        }


        [HttpGet("FilterProjects/{status}")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetAvtiveProjects(string status)
        {

            var projects = await _context.Projects.Include(e => e.ProjectManagerNavigation).
                Select(e =>
                    new ProjectDTO
                    {
                        Id = e.Id,
                        ProjectType = e.ProjectType,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        Status = e.Status,
                        ProjectManagerFullName = e.ProjectManagerNavigation != null ? e.ProjectManagerNavigation.FullName : null,
                    }
                ).Where(e => e.Status == status).ToListAsync();

            return projects;
        }

        //[HttpGet("GetInactiveProjects")]
        //public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetInavtiveProjects()
        //{

        //    var projects = await _context.Projects.Include(e => e.ProjectManagerNavigation).
        //        Select(e =>
        //            new ProjectDTO
        //            {
        //                Id = e.Id,
        //                ProjectType = e.ProjectType,
        //                StartDate = e.StartDate,
        //                EndDate = e.EndDate,
        //                Status = e.Status,
        //                ProjectManagerFullName = e.ProjectManagerNavigation != null ? e.ProjectManagerNavigation.FullName : null,
        //            }
        //        ).Where(e => e.Status == "Inactive").ToListAsync();

        //    return projects;
        //}

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProject(int id)
        {
            var projects = await _context.Projects.Include(e => e.ProjectManagerNavigation).
                Select(e =>
                    new ProjectDTO
                    {
                        Id = e.Id,
                        ProjectType = e.ProjectType,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        Status = e.Status,
                        ProjectManagerFullName = e.ProjectManagerNavigation != null ? e.ProjectManagerNavigation.FullName : null,
                    }
                ).Where(e => e.Id == id).ToListAsync();

            return projects;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
