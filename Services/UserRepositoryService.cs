using System;
using System.Threading.Tasks;

public class UserRepositoryService : IUserRepositoryService
{
    private readonly int _serviceId;
    private readonly IHasherService _hasherService;
    private List<User> Users = new List<User>{};
    private int idCounter = 0;

    public UserRepositoryService(IHasherService hasherService)
    {
        _serviceId = new Random().Next(100000,999999);
        _hasherService = hasherService;
        LogCreation($"Message: UserRepositoryService {_serviceId} created.");
    }

    public async Task<User?> Get(int id)
    {
        return Users.Find(u => u.Id == id);
    }

    public async Task<User?> GetByEmail(string username)
    {
        return Users.Find(u => u.Username == username);
    }

    public async Task<bool> Insert(User user)
    {
        if (Users.Any(u => u.Username == user.Username)) return false;
        idCounter++;
        user.SetId(idCounter);
        Users.Add(user);
        return true;
    }

    public async Task Remove(int id)
    {
        Users.RemoveAll(u => u.Id==id);
    }

    public async Task Edit(int id, User user)
    {
        Users.Find(u => u.Id==id).Username = user.Username;
        Users.Find(u => u.Id==id).Password = user.Password;
    }

    public async Task AssignPosts(int userId, Post post)
    {
        var user = Users.Find(u => u.Id == userId);
        LogCreation($"AssignPosts called for userId: {userId}, postId: {post.Id}");
        if (user != null)
        {
            LogCreation($"Assigning post with ID {post.Id} to user with ID {userId}");
            user.AddPostId(post.Id);
        }
        else
        {
            LogCreation($"User not found.");
        }
    }

    public async Task RemovePosts(int userId, Post post)
    {
        var user = Users.Find(u => u.Id == userId);
        if (user != null)
        {
            user.RemovePostId(post.Id);
        }
        else
        {
            LogCreation($"User not found.");
        }
    }

    public async Task<User?> Validate(string username, string password)
    {
        Console.WriteLine($"UserRepositoryService: Validating user with username: {username}, password: {password}");
        var user = Users.Find(u => u.Username == username);
        if(user != null && _hasherService.VerifyPassword(password, user.Password))
        {
            return user;
        }
        return null;
    }

    public async Task<List<User>> GetAll()
    {
        return Users;
    }

    public void LogCreation(string message){
        Console.WriteLine($"{message} - Service ID: {_serviceId}");
    }
    
}