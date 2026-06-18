public class User
{
    public int Id {get; set;}
    public string Username {get; set;}
    public string Password {get; set;}
    public Post? post;

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public void SetId(int id)
    {
        Id = id;
    }

}