namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }

    public Comment(int userId, string content, int postId)
    {
        UserId = userId;
        Content = content;
        PostId = postId;
    }
    public Comment(){}
}