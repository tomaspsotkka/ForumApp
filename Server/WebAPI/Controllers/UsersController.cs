using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public UsersController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserDto request)
    {
        try
        {
            await VerifyUsernameIsAvailableAsync(request.Username);
            User user = new(request.Username, request.Password);
            User created = await userRepository.AddAsync(user);
            UserDto dto = new()
            {
                Id = created.Id,
                Username = created.Username,
                Password = created.Password
            };
            return Created($"/Users/{dto.Id}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser([FromRoute] int id, [FromBody] UserDto request)
    {
        try
        {
            User? user = await userRepository.GetSingleAsync(id);
            if (user == null)
            {
                return NotFound($"User with id {id} not found.");
            }

            user.Username = request.Username;
            user.Password = request.Password;
            await userRepository.UpdateAsync(user);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("by-username/{username}")]
    public async Task<ActionResult<UserDto>> GetSingleUser([FromRoute] string username)
    {
        try
        {
            User? user = await userRepository.GetSingleAsync(username);
            if (user == null)
            {
                return NotFound($"User with Username '{username}' not found");
            }
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password
            };
            return Ok(userDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("by-id/{id}")]
    public async Task<ActionResult<UserDto>> GetSingleUser([FromRoute] int id)
    {
        try
        {
            User? user = await userRepository.GetSingleAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID '{id}' not found");
            }
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password
            };
            return Ok(userDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<UserDto>> GetManyUsers()
    {
        try
        {
            IQueryable<User> users = userRepository.GetMany();
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<UserDto>> DeleteUser([FromRoute] int id)
    {
        try
        {
            await userRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    private async Task VerifyUsernameIsAvailableAsync(string username)
    {
        User? existing = await userRepository.GetSingleAsync(username);
        if (existing != null)
        {
            throw new InvalidOperationException(
                $"Username '{username}' is already taken");
        }
    }
}