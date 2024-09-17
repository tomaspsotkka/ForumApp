using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    public List<User> users = new List<User>();

    public UserInMemoryRepository()
    {
        users.Add(new User("tomas", "nic123", 1));
        users.Add(new User("matej", "tytytytyty", 2));
        users.Add(new User("patrik", "kolobrnda", 3));
        users.Add(new User("sebastian", "lego123", 4));
    }

    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any()
            ? users.Max(u => u.Id) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? userToUpdate = users.SingleOrDefault(u => u.Id == user.Id);
        if (userToUpdate is null)
        {
            throw new InvalidOperationException(
                $"User with id {user.Id} not found");
        }
        users.Remove(userToUpdate);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? userToDelete = users.SingleOrDefault(u => u.Id == id);
        if (userToDelete is null)
        {
            throw new InvalidOperationException(
                $"User with id {id} not found");
        }
        users.Remove(userToDelete);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? userToGet = users.SingleOrDefault(u => u.Id == id);
        if (userToGet is null)
        {
            throw new InvalidOperationException(
                $"User with id {id} not found");
        }
        return Task.FromResult(userToGet);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }
}