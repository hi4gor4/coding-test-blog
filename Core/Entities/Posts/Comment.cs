using Core.Entities.Users;

namespace Core.Entities.Posts;

public class Comment :BaseEntity
{
    public long UserId { get; set; }
    public long PostId { get; set; }
    public string Content { get; set; } = null!;
    public Post? Post { get; set; }
    public User? User { get; set; }
}
