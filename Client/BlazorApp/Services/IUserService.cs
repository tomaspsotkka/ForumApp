using DTOs;
using Microsoft.AspNetCore.Mvc;


namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto request);
    public Task<UserDto> UpdateUserAsync(int id, UpdateUserDto request);
    public Task<UserDto> GetSingleUserAsync(string username, UserDto request);
    public Task<UserDto> GetSingleUserAsync(int id, UserDto request);
    public Task<ActionResult<UserDto>> GetManyUsersAsync(UserDto request);
    public Task<UserDto> DeleteUserAsync(UserDto request);
}