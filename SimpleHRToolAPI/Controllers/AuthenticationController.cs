using JWTGenerator;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHRToolAPI.HelperMethods;
using SimpleHRToolAPI.Models;
using SimpleHRToolAPI.Models.UserModel;
using System.Linq;

namespace SimpleHRToolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly SimpleHrtoolContext _context;
        private readonly IConfiguration _configuration;


        public AuthenticationController(SimpleHrtoolContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        [HttpPost("RegisterNewUser")]
        public async Task<ActionResult> RegisterNewUser(UserDTO userDTO)
        {
            
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            int roleID = await _context.Roles.Where(e => e.RoleName == userDTO.Role).Select(e => e.Id).FirstOrDefaultAsync();
            int employeeID = await _context.Employees.Where(e => e.FullName == userDTO.Employee).Select(e => e.Id).FirstOrDefaultAsync();


            _context.Users.Add(new User { Employee = employeeID, PasswordHash = passwordHash, UserName = userDTO.UserName, Role = roleID });

            await _context.SaveChangesAsync();


            return Ok("User Registered Succesfully");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoggedSessions>> Login(UserLogin userLogin)
        {
            var user = new UserDTO();

            //Check if user exists
            if(!_context.Users.Any(e => e.UserName == userLogin.UserName))
            {
                return BadRequest("User Or Password was Incorrect!");
            }
            //Select User if they exist
            else
            {
                user = await _context.Users.Include(e => e.EmployeeNavigation).Include(e => e.RoleNavigation).
                    Where(e => e.UserName == userLogin.UserName).Select(e => new UserDTO{ UserName = e.UserName, Password = e.PasswordHash, 
                        Employee = e.EmployeeNavigation != null ? e.EmployeeNavigation.FullName : null, 
                        Role = e.RoleNavigation !=null ? e.RoleNavigation.RoleName : null
                        }).FirstOrDefaultAsync();
            }

            //If password Hash is incorrect then return an error
            if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, user!.Password))
            {
                return BadRequest("User Or Password was Incorrect!");
            }

            //Section must be finished!
            DateTime dt = new DateTime().AddHours(1);
            

            //Create JWT Token if username and password is correct
            var CreateJWTToken = new CreateJWTToken(user.UserName, user.Role, _configuration.GetSection("AppSettings:Token").Value!,
                _configuration.GetSection("AppSettings:Issuer").Value!, _configuration.GetSection("AppSettings:Audience").Value!, dt);
            string CreateTokenAndSaveIt = CreateJWTToken.GenerateJWTToken();


            //Save JWT Token to a cookie file
            CookieCreator.CreateCookie(CreateTokenAndSaveIt, dt, Response);
            //_context.LoggedSessions.Add(new LoggedSessions { UserName = user.UserName, JWT = CreateTokenAndSaveIt });
            //_context.SaveChanges();

            //Change it later so Role and other attributes are also used
            return Ok(new LoggedSessions { UserName = user.UserName, JWT = CreateTokenAndSaveIt });
        }

        
    }
}
