/*using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private List<Comment> comments = new List<Comment>();
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(u => u.Id) + 1
            : 1;
        
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(u => u.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with id {comment.Id} not found");
        }
        comments.Remove(existingComment);
        comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(u => u.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException(
                $"Comment with id {id} not found");
        }
        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(u => u.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException(
                $"Comment with id {id} not found");
        }
        return Task.FromResult(commentToRemove);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }
}*/