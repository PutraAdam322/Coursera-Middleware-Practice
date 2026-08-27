public class Post
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int Id { get; set; }

    public Post(string title, string content)
    {
        Title = title;
        Content = content;
    }

}