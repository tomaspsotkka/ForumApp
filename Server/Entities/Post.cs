namespace Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; }
    public int CommentId { get; }

    public Post(string title, string body)
    {
        Title = title;
        Body = body;
        UserId = 1; //f
    }
}