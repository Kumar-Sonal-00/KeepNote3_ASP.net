using Microsoft.AspNetCore.Mvc;
//using KeepNote.Service;
//using KeepNote.Entities;
//using KeepNote.Exceptions;
using Entities;
using Exceptions;
using Service;

namespace KeepNote.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                _userService.CreateUser(user);
                return CreatedAtRoute("GetUserById", new { id = user.UserId }, user);
            }
            catch (UserAlreadyExistsException)
            {
                return Conflict("User with the same ID already exists.");
            }
        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] User user)
        {
            try
            {
                var loggedInUser = _userService.LoginUser(user.UserId, user.Password);
                if (loggedInUser != null)
                {
                    return Ok("User logged in successfully.");
                }
                else
                {
                    return NotFound("User not found.");
                }
            }
            catch (UserNotFoundException)
            {
                return NotFound("User not found.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                _userService.UpdateUser(id, user);
                return Ok("User updated successfully.");
            }
            catch (UserNotFoundException)
            {
                return NotFound("User not found.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok("User deleted successfully.");
            }
            catch (UserNotFoundException)
            {
                return NotFound("User not found.");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var user = _userService.GetUserById(userId);
                return Ok(user);
            }
            catch (UserNotFoundException)
            {
                return NotFound("User not found.");
            }
        }
    }
}


/*
 * Define a handler method which will create a specific user by reading the
 * Serialized object from request body and save the user details in a User table
 * in the database. This handler method should return any one of the status
 * messages basis on different situations: 1. 201(CREATED) - If the user created
 * successfully. 2. 409(CONFLICT) - If the userId conflicts with any existing
 * user
 * 
 * 
 * This handler method should map to the URL "/api/user/register" using HTTP POST
 * method
 */

/*
 * Define a handler method which will login a specific user by reading the
 * Serialized object from request body and validate the userId and Password
 * from User table in the database. This handler method should return any one of 
 * the status messages basis on different situations: 
 * 1. 200(OK) - If the user successfully logged in. 
 * 2. 404(NOTFOUND) -If the user with specified userId is not found.
 * 
 * This handler method should map to the URL "/api/user/login" using HTTP POST
 * method
 */

/*
 * Define a handler method which will update a specific user by reading the
 * Serialized object from request body and save the updated user details in a
 * user table in database handle exception as well. This handler method should
 * return any one of the status messages basis on different situations: 1.
 * 200(OK) - If the user updated successfully. 2. 404(NOT FOUND) - If the user
 * with specified userId is not found. 
 * 
 * This handler method should map to the URL "/api/user/{id}" using HTTP PUT method.
 */

/*
 * Define a handler method which will delete a user from a database.
 * 
 * This handler method should return any one of the status messages basis on
 * different situations: 1. 200(OK) - If the user deleted successfully from
 * database. 2. 404(NOT FOUND) - If the user with specified userId is not found.
 * 
 * This handler method should map to the URL "/api/user/{id}" using HTTP Delete
 * method" where "id" should be replaced by a valid userId without {}
 */

/*
 * Define a handler method which will show details of a specific user handle
 * UserNotFoundException as well. This handler method should return any one of
 * the status messages basis on different situations: 1. 200(OK) - If the user
 * found successfully. 2. 404(NOT FOUND) - If the user with specified
 * userId is not found. This handler method should map to the URL "/api/user/{userId}"
 * using HTTP GET method where "id" should be replaced by a valid userId without
 * {}
 */

