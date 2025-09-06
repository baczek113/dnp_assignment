namespace Entities;

public class Post
{
    private int id { get; set; }
    private string title { get; set; }
    private string body { get; set; }
    private User author { get; set; }
}