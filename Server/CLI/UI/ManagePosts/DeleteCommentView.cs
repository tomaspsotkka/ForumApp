/*using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class DeleteCommentView
{
    private readonly ICommentRepository commentRepository;
    public DeleteCommentView(ICommentRepository commentRepository)
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
        await commentRepository.DeleteAsync(commentId);
        Console.WriteLine($"Comment with id {commentId} was deleted.");
    }
}*/