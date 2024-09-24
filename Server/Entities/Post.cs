namespace Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; }

    public Post(string title, string body)
    {
        Title = title;
        Body = body;
        UserId = 1; //f
    }
    
    public Post(string title, string body, int id)
    {
        Title = title;
        Body = body;
        UserId = 1;
        Id = id;
    }
    public Post(){}
}