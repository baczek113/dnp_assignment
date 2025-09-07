namespace Entities;

public class Comment
{
    private string Body { get; set; }
    private Post Post { get; set; }
    private int AuthorId { get; set; }
}