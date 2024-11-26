using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task<bool> DeleteAsync(int id);
    Task<Post> GetSingleAsync(int id);
    Task<Post> GetSingleAsync(string title);
    IQueryable<Post> GetMany();
}