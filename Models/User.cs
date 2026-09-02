public class User
{
    public int Id {get; set;}
    public string Username {get; set;}
    public string Password {get; set;}
    public List<int> PostIds {get; set;} = new List<int>();

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public void SetId(int id)
    {
        Id = id;
    }
    public void AddPostId(int postId)
    {
        PostIds.Add(postId);
    }

    public void RemovePostId(int postId)
    {
        PostIds.Remove(postId);
    }

}