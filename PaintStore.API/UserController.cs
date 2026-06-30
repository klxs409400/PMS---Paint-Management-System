using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintStore.Model;

namespace PaintStore.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private List<User> users;

        public UserController()
        {
            users = new List<User>();
        }

        [HttpGet("User/GetUsers")]
        public ActionResult GetUsers([FromQuery]int? PageNumber, [FromQuery] int? PageSize )
        {
            if(PageNumber.HasValue && PageSize.HasValue)
            {
                var user = users.OrderBy(p => p.Id).Skip((PageNumber.Value-1) * PageSize.Value).Take(PageSize.Value).ToList();
                return Ok(user);
            }
            return Ok(users);
        }

        [HttpGet("User/GetUserById")]
        public ActionResult GetUserById([FromQuery] int userId)
        {
            var user = users.Where(p => p.Id == userId).ToList();
            if(user.Count > 0)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpGet("User/GetUserByName")]
        public ActionResult GetUserByName([FromQuery] string userName)
        {
            var user = users.Where(p => p.Name == userName).ToList();
            if(user.Count > 0)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpGet("User/GetUserByEmail")]
        public ActionResult GetUserByEmail([FromQuery] string userEmail)
        {
            var user = users.Where(p => p.Email == userEmail).ToList();
            if(user.Count > 0)
            {
                return Ok(user);
            }
            return NotFound();
        }
    }
}
