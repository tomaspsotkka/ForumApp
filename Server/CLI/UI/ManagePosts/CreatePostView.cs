/*using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private List<Comment> comments;
    

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
        comments = new List<Comment>();
    }

    /*public async Task ShowAsync()
    {
        Console.WriteLine("Enter the title of the post:");
        string? title = Console.ReadLine();
        
        Console.WriteLine("Enter the description of the post:");
        string? description = Console.ReadLine();

        Console.Clear();
        
        var newPost = new Post(title,description);
        await postRepository.AddAsync(newPost);
        
        Console.WriteLine($"Post with Id: {newPost.Id} has been created !");
    }#1#
}*/