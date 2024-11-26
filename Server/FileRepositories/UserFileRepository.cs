using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    public async Task<User> AddAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        var users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;
        user.Id = maxId + 1;
        users.Add(user);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User>? users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        users.Remove(existingUser);
        users.Add(user);
        users = users.OrderBy(u => u.Id).ToList();
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User>? users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        User? existingUser = users.SingleOrDefault(u => u.Id == id);
        if (existingUser is null)
        {
            return false;
        }
        users.Remove(existingUser);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return true;
    }

    public async Task<User?> GetSingleAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User>? users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        User? singleUserGet = users.SingleOrDefault(u => u.Id == id);
        return singleUserGet;
    }

    public async Task<User?> GetSingleAsync(string username)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User>? users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        User? singleUserGet = users.SingleOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)); //makes sure the username is not the same by ignoring the case
        return singleUserGet;
    }
    
    public IQueryable<User> GetMany()
    {
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        return users.AsQueryable();
    }
}