using ApiUsers.DTO;
using ApiUsers.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;

        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        [HttpGet("Return")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userInterface.GetUsers();

            if (users.Status == false)
            {
                return NotFound(users);
            }

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var users = await _userInterface.GetUserById(userId);

            if (users.Status == false)
            {
                return NotFound(users);
            }

            return Ok(users);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
        {
            var users = await _userInterface.CreateUser(createUserDTO);

            if (users.Status == false)
            {
                return BadRequest(users);
            }

            return Ok(users);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> EditUser(EditUserDTO editUserDTO)
        {
            var users = await _userInterface.EditUser(editUserDTO);

            if (users.Status == false)
            {
                return BadRequest(users);
            }

            return Ok(users);
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> RemoveUser(int userId)
        {
            var users = await _userInterface.RemoveUser(userId);

            if (users.Status == false)
            {
                return BadRequest(users);
            }

            return Ok(users);
        }
    }
}
