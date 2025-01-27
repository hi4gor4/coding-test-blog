using Core.Entities.Users;

namespace Core.Entities.Posts;

public class Like : BaseEntity
{
    public long PostId { get; set; }
    public long UserId { get; set; }
    public Post Post { get; set; } = new();
    public User User { get; set; } = new();
}
