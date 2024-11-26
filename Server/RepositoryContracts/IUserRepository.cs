using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task<User?> GetSingleAsync(int id);
    Task<User?> GetSingleAsync(string username);
    IQueryable<User> GetMany();
    Task<bool> DeleteAsync(int id);

}