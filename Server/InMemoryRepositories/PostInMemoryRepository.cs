/*using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    public List<Post> posts = new List<Post>();

    public PostInMemoryRepository()
    {
        posts.Add(new Post("Post nnnnnj", "njjjjj", 1));
        posts.Add(new Post("Dog", "nice dog", 2));
        posts.Add(new Post("How to write", "idk search on google", 3));
        posts.Add(new Post("C# assignment", "done don't worry", 4));
    }
    
    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.Id}' not found");
        }
        posts.Remove(existingPost);
        posts.Add(post);
        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            return false;
        }
        posts.Remove(postToRemove);
        return true;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? singlePostGet = posts.SingleOrDefault(p => p.Id == id);
        if (singlePostGet is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found"); 
        }
        return Task.FromResult(singlePostGet);
    }
    public Task<Post> GetSingleAsync(string title)
    {
        Post? singlePostGet = posts.SingleOrDefault(p => p.Title == title);
        if (singlePostGet is null)
        {
            throw new InvalidOperationException(
                $"Post with title '{title}' not found"); 
        }
        return Task.FromResult(singlePostGet);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
}*/