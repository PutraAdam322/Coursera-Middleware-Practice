public class Post
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;

    public Post(string title, string content, int userId)
    {
        Title = title;
        Content = content;
        UserId = userId;
    }
    public void SetId(int id)
    {
        Id = id;
    }

}