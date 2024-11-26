/*using CLI.UI.ManagePosts;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    public readonly IPostRepository postRepository;
    public readonly IUserRepository userRepository;
    public readonly ICommentRepository commentRepository;

    public ManageUsersView(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.WriteLine("1. Create user");
            Console.WriteLine("2. List of users");
            Console.WriteLine("3. Get specific user");
            Console.WriteLine("4. Delete user");
            Console.WriteLine("0. Back");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.Clear();
                    await CreateUserAsync();
                    break;
                case "2":
                    Console.Clear();
                    await ListUsersAsync();
                    break;
                case "3":
                    Console.Clear();
                    await SingleUserAsync();
                    break;
                case "4":
                    Console.Clear();
                    await DeleteUserAsync();
                    break;
                case "0":
                    Console.Clear();
                    await MainMenuAsync();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
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
    
    private async Task SingleUserAsync()
    {
        var singleUserView = new SingleUserView(userRepository);
        await singleUserView.ShowAsync();
    }

    private async Task DeleteUserAsync()
    {
        var deleteUserView = new DeleteUserView(userRepository);
        await deleteUserView.ShowAsync();
    }
    
    private async Task MainMenuAsync()
    {
        var mainMenu = new CliApp(postRepository, userRepository, commentRepository);
        await mainMenu.StartAsync();
    }
}*/