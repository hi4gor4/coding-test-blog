using Microsoft.AspNetCore.Mvc;

public class PostModel
{
    public string Content { get; set; }
    public string UserId { get; set; }
}

public class CreatePostViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View(new PostModel());
    }
}