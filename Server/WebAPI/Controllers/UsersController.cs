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
    public async Task<ActionResult<UserDto>> AddUser([FromBody] UserDto request)
    {
        try
        {
            await VerifyUsernameIsAvailableAsync(request.Username);
            User user = new(request.Username, request.Password);
            User created = await userRepository.AddAsync(user);
            UserDto dto = new()
            {
                Id = created.Id,
                Username = created.Username
            };
            return Created($"/Users/{dto.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateUser([FromRoute] int id, [FromBody] UserDto request)
    {
        try
        {
            User user = await userRepository.GetSingleAsync(id);
            if (user == null)
            {
                return Results.NotFound();
            }

            user.Username = request.Username;
            user.Password = request.Password;
            return Results.NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetSingleUser([FromRoute] int id)
    {
        try
        {
            User user = await userRepository.GetSingleAsync(id);
            return Results.Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public async Task<IResult> GetManyUsers()
    {
        try
        {
            IQueryable<User> users = userRepository.GetMany();
            return Results.Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteUser([FromRoute] int id)
    {
        try
        {
            await userRepository.DeleteAsync(id);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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