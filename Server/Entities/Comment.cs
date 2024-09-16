namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public int PostId { get; }
    public int UserId { get; }

    public Comment(int userId, string body, int postId)
    {
        UserId = userId;
        Body = body;
        PostId = postId;
    }
    public Comment(){}
}