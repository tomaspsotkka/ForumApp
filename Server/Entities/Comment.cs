namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int PostId { get; }
    public int UserId { get; }

    public Comment(int userId, string content, int postId)
    {
        UserId = userId;
        Content = content;
        PostId = postId;
    }
    public Comment(){}
}