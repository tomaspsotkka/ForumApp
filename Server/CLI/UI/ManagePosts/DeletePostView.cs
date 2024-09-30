using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class DeletePostView
{
    private readonly IPostRepository postRepository;

    public DeletePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Choose id: ");
        var id = Console.ReadLine();
        int postId;
        Int32.TryParse(id, out postId);
        
        Console.Clear();
        await postRepository.DeleteAsync(postId);
        Console.WriteLine($"Post with id {postId} was deleted.");
    }
}