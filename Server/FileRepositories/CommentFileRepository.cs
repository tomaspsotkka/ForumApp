using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";
    public List<Comment> comments = new List<Comment>();

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment>? comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment>? comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID {comment.Id} not found");
        }
        comments.Remove(existingComment);
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment>? comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }
        this.comments.Remove(existingComment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment>? comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson);
        Comment? singleCommentGet = comments.SingleOrDefault(c => c.Id == id);
        if (singleCommentGet is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID {id} not found"); 
        }
        return await Task.FromResult(singleCommentGet);;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }
}