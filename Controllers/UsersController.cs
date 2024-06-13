using asp_net_ecommerce_api.Models;
using asp_net_ecommerce_api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace  asp_net_ecommerce_api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase{

    private readonly IUserRepository _userRepository;

    // bind user controller to user repository
    public  UsersController(IUserRepository userRepository){
        _userRepository = userRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }
  [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user){       
        try
        {
             if (user == null)
                {
                    return BadRequest("User object is null");
                }

               // Check if the user already exists
                 var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
                if (existingUser != null)
                {
                    return Conflict("User already exists");
                }

                // Create the user
                var createdUser = await _userRepository.CreateUserAsync(user);

                // Generate JWT token
                var token =  _userRepository.GenerateJwtToken(user);
                return CreatedAtAction(nameof(Register), new { Token = token });
        }
        catch (Exception ex)
        {            
           return StatusCode(500, $"An error occurred while registering the user: {ex.Message}");
        }        
    }

    [HttpPost("login")]
public async Task<ActionResult<string>> Login(User model)
{
    try
    {
        var user = await _userRepository.GetUserByUsernameAsync(model.Username);

        if (user == null || !_userRepository.IsValidPassword(model.Password, user.Password))
        {
            return Unauthorized("Invalid username or password.");
        }

        var token = _userRepository.GenerateJwtToken(user);
        return Ok(token);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}
}
