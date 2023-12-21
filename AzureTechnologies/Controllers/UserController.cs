using Microsoft.AspNetCore.Mvc;
using System.Net;
using AzureTechnologies.Application.Utils;
using AzureTechnologies.Domain.API;
using AzureTechnologies.Domain.Interfaces.Service;
using AzureTechnologies.Domain.Models;

namespace AzureTechnologies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest(APIResponse.Error("Invalid user id"));
            }

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(APIResponse.Error($"User with id {id} not found"));
            }

            return Ok(APIResponse.StatusOk(data: user));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> PostAsync([FromBody] UserDTO user)
        {
            if (!Validator.IsEmailValid(user?.Email))
            {
                return UnprocessableEntity(APIResponse.Error("Invalid email address."));
            }
            else if (!Validator.IsStrongPassword(user?.Password))
            {
                return UnprocessableEntity(APIResponse.Error("Password should contains at 6 characters with at least one lowercase letter, one uppercase letter, one digit, and one special character."));
            }
            else if ((await _userService.IsEmailExistsAsync(user?.Email)).GetValueOrDefault(false))
            {
                return Conflict(APIResponse.Error($"The user with {user?.Email} email ID already exists."));
            }

            var newUserId = await _userService.AddAsync(user);

            if (newUserId.HasValue)
            {
                return Created(nameof(GetAsync), APIResponse.StatusOk("Registration successful", new { id = newUserId }));
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, APIResponse.InternalServerError());
        }
    }
}