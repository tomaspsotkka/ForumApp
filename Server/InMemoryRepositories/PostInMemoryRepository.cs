using System.Formats.Tar;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository
{
    public List<Post> posts;
    
    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }
}