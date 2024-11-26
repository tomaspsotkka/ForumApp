using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task<bool> DeleteAsync(int id, int postId);
    Task<Comment> GetSingleAsync(int id);
    IQueryable<Comment> GetMany();
}