namespace ApiContracts.Post;

public class CreatePostDto
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int AuthorId { get; set; }
}