using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class UpdateCommentView
{
    private readonly ICommentRepository commentRepository;
    
    public UpdateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }
    
    public async Task ShowAsync()
    {
        Console.WriteLine("Choose id: ");
        var id = Console.ReadLine();
        int commentId;
        Int32.TryParse(id, out commentId);
        
        Console.Clear();
        var comment = await commentRepository.GetSingleAsync(commentId);
        Console.WriteLine("Enter new content: ");
        comment.Content = Console.ReadLine();
        await commentRepository.UpdateAsync(comment);
        Console.WriteLine("Comment updated.");
    }
}