using Microsoft.AspNetCore.Mvc;

public class PostListModel
{
    public string Username { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}


public class PostViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string username, string content, DateTime createdAt)
    {
        var model = new PostListModel
        {
            Username = username,
            Content = content,
            CreatedAt = createdAt
        };

        return View(model);
    }
}
