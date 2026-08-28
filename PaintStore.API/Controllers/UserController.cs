using Microsoft.AspNetCore.Mvc;
using PaintStore.API.Dto;
using PaintStore.API.Services;
using PaintStore.Model.Models;

namespace PaintStore.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(UserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetAllUser()
        {
            List<User> getAllUser = _userService.GetAllUsers();
            var resultUsers = new List<UserResponseDto>();
            foreach (var u in getAllUser)
            {
                UserResponseDto userResponseDto = new UserResponseDto();
                userResponseDto.Id = u.Id;
                userResponseDto.Email = u.Email;
                userResponseDto.Name = u.Name;
                userResponseDto.Phone = u.Phone;
                resultUsers.Add(userResponseDto);
            }
            return Ok(resultUsers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            User getUser = await _userService.GetUserById(id);
            var resultUser = new UserResponseDto();
            resultUser.Email = getUser.Email;
            resultUser.Id = getUser.Id;
            resultUser.Name = getUser.Name;
            resultUser.Phone = getUser.Phone;
            return Ok(resultUser);
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody] UserCreateRequestDto userCreateRequestDto)
        {
            _logger.LogInformation("Received request to create user with email {Email}", userCreateRequestDto.Email);

            var createUser = _userService.CreateUser(userCreateRequestDto.Name, userCreateRequestDto.Email, userCreateRequestDto.Phone);
            var resultUser = new UserResponseDto();
            resultUser.Email = createUser.Email;
            resultUser.Name = createUser.Name;
            resultUser.Phone = createUser.Phone;
            resultUser.Id = createUser.Id;

            _logger.LogInformation("Created user with id {Id}", resultUser.Id);
            return Created($"/api/users/{resultUser.Id}", resultUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserUpdateRequestDto userUpdateRequestDto)
        {
            _logger.LogInformation("Received request to update user with id {Id}", id);
            await _userService.UpdateUser(id, userUpdateRequestDto.Name, userUpdateRequestDto.Email, userUpdateRequestDto.Phone);
            _logger.LogInformation("Updated user with id {Id}", id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            _logger.LogInformation("Received request to delete user with id {Id}", id);
            _userService.DeleteUser(id);
            _logger.LogInformation("Deleted user with id {Id}", id);
            return Ok();
        }
    }


}
