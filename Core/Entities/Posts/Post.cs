using Core.Entities.Users;
namespace Core.Entities.Posts;


public class Post: BaseEntity
{
    public string Content { get; set; } = null!;
    public long UserId { get; set; }
    public bool IsUpdated { get; set; }

    public User User { get; set; } 

}
