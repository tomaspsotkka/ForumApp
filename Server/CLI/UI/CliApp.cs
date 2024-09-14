using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    public readonly IPostRepository postRepository;
    public readonly IUserRepository userRepository;

    public CliApp(IPostRepository postRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }
    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("Select an option: ");
            Console.WriteLine("POST OPTIONS");
            Console.WriteLine("1. Create post");
            Console.WriteLine("2. List of posts");
            Console.WriteLine("3. Get specific post \n");
            Console.WriteLine("USER OPTIONS");
            Console.WriteLine("4. Create user");
            Console.WriteLine("5. List of users \n");
            Console.WriteLine("0. Exit");
            Console.WriteLine("Enter option: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    await CreatePostAsync();
                    break;
                case "2":
                    Console.Clear();
                    await ListPostsAsync();
                    break;
                case "3":
                    Console.Clear();
                    await SinglePostAsync();
                    break;
                case "4":
                    Console.Clear();
                    await CreateUserAsync();
                    break;
                case "5":
                    Console.Clear();
                    await ListUsersAsync();
                    break;
                case "0":
                    Console.Clear();
                    Console.WriteLine("Exiting...");
                    return;
                default: 
                    Console.Clear();
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }
    private async Task CreatePostAsync()
    {
        var createPostView = new CreatePostView(postRepository);
        await createPostView.ShowAsync();
    }

    private async Task ListPostsAsync()
    {
        var listPostsView = new ListPostsView(postRepository);
        await listPostsView.ShowAsync();
    }
    
    private async Task CreateUserAsync()
    {
        var createUserView = new CreateUserView(userRepository);
        await createUserView.ShowAsync();
    }
    
    private async Task ListUsersAsync()
    {
        var listUsersView = new ListUsersView(userRepository);
        await listUsersView.ShowAsync();
    }
    
    private async Task SinglePostAsync()
    {
        var singlePostView = new SinglePostView(postRepository);
        await singlePostView.ShowAsync();
    }
}