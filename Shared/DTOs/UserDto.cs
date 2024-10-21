namespace DTOs;

public class UserDto
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public string Password { get; set; }
}