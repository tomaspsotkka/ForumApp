using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreateCommentView
{
    public readonly ICommentRepository commentRepository;
    public readonly IPostRepository postRepository;
    public readonly IUserRepository userRepository;
    public readonly int postId;

    public CreateCommentView(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository, int postId)
    {
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        this.postId = postId;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Enter your user id:");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid user ID.");
            return;
        }
        
        Console.WriteLine("Enter your comment:");
        string? comment = Console.ReadLine();
        
        if (string.IsNullOrEmpty(comment))
        {
            Console.WriteLine("Comment cannot be empty.");
            return;
        }
        Comment newComment = new Comment(userId, comment, postId);
        await commentRepository.AddAsync(newComment);
        Console.WriteLine("Comment added successfully.");
    }
}