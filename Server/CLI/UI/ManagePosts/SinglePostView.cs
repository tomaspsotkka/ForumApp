using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;

    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        try
        {
            Console.WriteLine("Enter post id: ");
            var postId = Console.ReadLine();
            int id;
            Int32.TryParse(postId, out id);
        
            Console.Clear();
            var post = await postRepository.GetSingleAsync(id);
                Console.WriteLine($"{post.Id}. Title: {post.Title} \n" +
                                  $"Content: {post.Body} \n");
                
            var comments = commentRepository.GetMany().ToList();
        
            if (comments.Any())
            {
                Console.WriteLine("Comments: ");
                foreach (var comment in comments)
                {
                    var user = await userRepository.GetSingleAsync(comment.UserId);   
                    Console.WriteLine($"{user.Username}: {comment.Content}");
                }
            }
            else
            {
                Console.WriteLine("No comments yet.");
            }

            while (true)
            {
                Console.WriteLine("1. Comment post");
                Console.WriteLine("2. Back");
                Console.WriteLine("0. Exit");
                var comment = Console.ReadLine();

                switch (comment)
                {
                    case "1":
                        Console.Clear();
                        await AddCommentAsync(id);
                        break;
                    case "2":
                        Console.Clear();
                        await MainMenuAsync();
                        break;
                    case "0":
                        Console.Clear();
                        return;
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    private async Task AddCommentAsync(int postId)
    {
        var addCommentView = new CreateCommentView(commentRepository, userRepository, postId);
        await addCommentView.ShowAsync();
    }
    
    private async Task MainMenuAsync()
    {
        var mainMenu = new CliApp(postRepository, userRepository, commentRepository);
        await mainMenu.StartAsync();
    }
}