using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeApi.Model;
using PracticeCore.Dto;
using PracticeCore.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userservice;

        public UserController(IUserService userService)
        {
            _userservice = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {

            var newUser = new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            var result = await _userservice.AddUser(newUser);


            if (result != null)

            {
                return Ok(user);
            }
            return BadRequest("User can not be Created");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInDto signInDto)
        { 

            try
            {
                //throw new Exception("Server Error");
                var result = await _userservice.Login(signInDto);

                if (result == null)
                {
                    return BadRequest("Invalid User; Email or Password Not Correct !!!");
                }

                return StatusCode(201, new { JwtToken =  result.Token });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ ex.Message }" });

            }
           
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {

            var users = await _userservice.GetAllUser();
            if (users == null)
            {
                return NotFound("No User Added");
            }

            return Ok(users);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {

            var users = await _userservice.FindByEmail(email);
            if (users == null)
            {
                //return NotFound("User Not Found");
                return StatusCode(404, new { Message = "User not found !" });
               
            }

            //return Ok(users);
            return StatusCode (200, users);
        }

        [HttpPatch("{email}")]
        public async Task<IActionResult> ResetPassword([FromBody]PasswordDto passwordDto, string email)
        {
            try
            {
                var result = await _userservice.UpdatePassWord(passwordDto, email);
                if (result)
                {
                    //return NotFound("User Not Found");
                    return StatusCode(200, new { Message = "Password Updated Successfully" });

                }

                //return Ok(users);
                return StatusCode(400, new { Messegae = "Oooppss Error ! Password not updated !" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

           
        }

        [HttpGet("email")]
        public async Task<IActionResult> GetByEmail(string email)
        {

            var users = await _userservice.FindUserByEmail();
            if (!users.Any())
            {
                return NotFound("User Not Found");
               
            }
                return Ok(users);

        }
    }
}
