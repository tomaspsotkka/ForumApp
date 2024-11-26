/*using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ShowAsync()
    {
        var posts = postRepository.GetMany().ToList();
        
        if (!posts.Any())
        {
            Console.WriteLine("No posts available.");
            return;
        }

        Console.Clear();
        Console.WriteLine("List of Posts:");
        foreach (var post in posts)
        {
            Console.WriteLine($"{post.Id}. Title: {post.Title}");
            Console.WriteLine($"Content: {post.Body} \n");
        }
    }
}*/