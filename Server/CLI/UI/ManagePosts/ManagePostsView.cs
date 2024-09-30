using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    public readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    public readonly ICommentRepository commentRepository;

    public ManagePostsView(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.WriteLine("1. Create post");
            Console.WriteLine("2. List of posts");
            Console.WriteLine("3. Get specific post");
            Console.WriteLine("4. Delete post");
            Console.WriteLine("0. Back");
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
                    await DeletePostAsync();
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
    
    private async Task SinglePostAsync()
    {
        var singlePostView = new SinglePostView(postRepository, commentRepository, userRepository);
        await singlePostView.ShowAsync();
    }

    private async Task DeletePostAsync()
    {
        var deletePostView = new DeletePostView(postRepository);
        await deletePostView.ShowAsync();
    }

    private async Task MainMenuAsync()
    {
        var mainMenu = new CliApp(postRepository, userRepository, commentRepository);
        await mainMenu.StartAsync();
    }
}