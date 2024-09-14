using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;

    public SinglePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ShowAsync()
    {
        
        Console.WriteLine("Enter Post Id: ");
        var postId = Console.ReadLine();
        int id;
        Int32.TryParse(postId, out id);
        
        Console.Clear();
        var post = await postRepository.GetSingleAsync(id);
        Console.WriteLine($"Post with id {id}: ");
        Console.WriteLine($"ID: {post.Id} \n" +
                          $"Title: {post.Title} \n" +
                          $"Content: {post.Body} \n");
    }
}