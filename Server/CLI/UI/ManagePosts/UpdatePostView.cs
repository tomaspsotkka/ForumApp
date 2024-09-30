using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class UpdatePostView
{
    private readonly IPostRepository postRepository;
    private readonly int postId;

    public UpdatePostView(IPostRepository postRepository, int postId)
    {
        this.postRepository = postRepository;
        this.postId = postId;
    }
    
    public async Task ShowAsync()
    {
        Console.Clear();
        var post = await postRepository.GetSingleAsync(postId);
        Console.WriteLine("Enter new title: ");
        post.Title = Console.ReadLine();
        Console.WriteLine("Enter new content: ");
        post.Body = Console.ReadLine();
        await postRepository.UpdateAsync(post);
        Console.WriteLine("Post updated.");
    }
}