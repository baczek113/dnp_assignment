namespace Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Post> Posts { get; set; }
    public List<Comment> Comments { get; set; }

    public User(string username, string password)
    {
        this.Username = username;
        this.Password = password;
        this.Id = -1;
    }

    public User(){}
}