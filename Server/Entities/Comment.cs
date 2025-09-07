namespace Entities;

public class Comment
{
    public string Body { get; set; }
    public Post Post { get; set; }
    public int AuthorId { get; set; }
}