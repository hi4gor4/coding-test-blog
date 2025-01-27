using Core.Entities.Posts;
namespace Core.Entities.Users;

public class User: BaseEntity
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Document { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime BirthDate { get; set; }

    public List<Post> Posts { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}
