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
                
            var comments = commentRepository.GetMany().Where(c => c.PostId == id).ToList();
        
            if (comments.Any())
            {
                Console.WriteLine("Comments: ");
                foreach (var comment in comments)
                {
                    var user = await userRepository.GetSingleAsync(comment.UserId);   
                    Console.WriteLine($"{comment.Id}.{user.Username}: {comment.Content}");
                }
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("No comments yet.");
            }

            while (true)
            {
                Console.WriteLine("1. Comment post");
                Console.WriteLine("2. Update post");
                Console.WriteLine("3. Delete comment");
                Console.WriteLine("4. Update comment");
                Console.WriteLine("0. Back");
                var comment = Console.ReadLine();

                switch (comment)
                {
                    case "1":
                        Console.Clear();
                        await AddCommentAsync(id);
                        break;
                    case "2":
                        Console.Clear();
                        await UpdatePostAsync(id);
                        break;
                    case "3":
                        Console.Clear();
                        await DeleteCommentAsync(id);
                        break;
                    case "4":
                        Console.Clear();
                        await UpdateCommentAsync(id);
                        break;
                    case "0":
                        Console.Clear();
                        await ManagePostsAsync();
                        break;
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
        var addCommentView = new CreateCommentView(commentRepository, postId);
        await addCommentView.ShowAsync();
    }
    private async Task UpdatePostAsync(int postId)
    {
        var updatePostView = new UpdatePostView(postRepository, postId); //Not taking in consoderation the UserId so
                                                                             //anyone can update any post (not good)
        await updatePostView.ShowAsync();
    }
    private async Task DeleteCommentAsync(int postId)
    {
        var deleteCommentView = new DeleteCommentView(commentRepository);
        await deleteCommentView.ShowAsync();
    }
    private async Task UpdateCommentAsync(int id)
    {
        var updateCommentView = new UpdateCommentView(commentRepository);
        await updateCommentView.ShowAsync();
    }
    private async Task ManagePostsAsync()
    {
        var managePostsView = new ManagePostsView(postRepository, userRepository, commentRepository);
        await managePostsView.ShowAsync();
    }
}