namespace Entities;

public class Comment
{
    private string body { get; set; }
    private Post post { get; set; }
    private User author { get; set; }
}