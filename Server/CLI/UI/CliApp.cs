using CLI.UI.ManagePosts;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    public readonly IPostRepository postRepository;

    public CliApp(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    public async Task StartAsync()
    {
        Console.WriteLine("Starting the application...");
        Console.WriteLine("1. Create post");
        Console.WriteLine("2. List of posts");
        Console.WriteLine("0.Exit");
        Console.WriteLine("Enter option: ");
        string? input = Console.ReadLine();

        /*switch (input)
        {
            case "1":
                await CreatePostAsync();
                break;
            case "2":
                
        }*/
    }

    private async Task CreatePostAsync()
    {
        var createPostView = new CreatePostView(postRepository);
        await createPostView.ShowAsync();
    }
}