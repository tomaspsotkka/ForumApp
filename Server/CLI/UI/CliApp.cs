/*using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    public readonly IPostRepository postRepository;
    public readonly IUserRepository userRepository;
    public readonly ICommentRepository commentRepository;

    public CliApp(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
    }
    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("1. Manage Posts");
            Console.WriteLine("2. Manage Users");
            Console.WriteLine("0. Exit");
            Console.WriteLine("Enter option: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    await ManagePostsAsync();
                    break;
                case "2":
                    Console.Clear();
                    await ManageUsersAsync();
                    break;
                case "0":
                    Console.Clear();
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
                default: 
                    Console.Clear();
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }
    private async Task ManagePostsAsync()
    {
        var managePostsView = new ManagePostsView(postRepository, userRepository, commentRepository);
        await managePostsView.ShowAsync();
    }
    private async Task ManageUsersAsync()
    {
        var manageUsersView = new ManageUsersView(userRepository, postRepository, commentRepository);
        await manageUsersView.ShowAsync();
    }
}*/