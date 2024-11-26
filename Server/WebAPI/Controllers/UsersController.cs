using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<CreateUserDto>> CreateUser([FromBody] CreateUserDto request)
    {
        try
        {
            await VerifyUsernameIsAvailableAsync(request.Username);
            User user = new(request.Username, request.Password);
            User created = await userRepository.AddAsync(user);
            CreateUserDto dto = new()
            {
                Username = created.Username,
                Password = created.Password
            };
            return Created($"/Users/", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateUserDto>> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDto request)
    {
        try
        {
            User? user = await userRepository.GetSingleAsync(id);
            if (user == null)
            {
                return NotFound($"User with id {id} not found.");
            }
            if (!string.Equals(user.Username, request.Username, StringComparison.OrdinalIgnoreCase))
            {
                await VerifyUsernameIsAvailableAsync(request.Username);
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
            if (users is null)
            {
                return NotFound("No users found");
            }
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
            bool deleted = await userRepository.DeleteAsync(id);
            if (deleted is false)
            {
                return NotFound($"User with ID '{id}' not found");
            }
            
            return Ok($"User with ID {id} successfully deleted."); //succesfully deleted (hopefully)
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