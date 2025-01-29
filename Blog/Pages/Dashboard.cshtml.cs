using AutoMapper;
using Core.Entities.Posts;
using Core.Entities.Users;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class DashboardModel : PageModel
{
    [BindProperty]
    public PostModel PostModel { get; set; }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string BirthDate { get; private set; }
    public string Phone { get; private set; }

    public List<Post> Posts { get; private set; } = new List<Post>();

    public string CurrentUserId { get; private set; }

    private readonly IPostService _postService;
    private readonly ILikeService _likeService;
    private readonly IMapper _mapper;

    public DashboardModel(IPostService postService,
        ILikeService likeService,
        IMapper mapper)
    {
        _postService = postService;
        _likeService = likeService;
        _mapper = mapper;
    }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        var claims = User.Claims;

        FullName = claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
        Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        BirthDate = claims.FirstOrDefault(c => c.Type == "BirthDate")?.Value;
        Phone = claims.FirstOrDefault(c => c.Type == "Phone")?.Value;

        CurrentUserId = claims.FirstOrDefault(c => c.Type == "Id")?.Value;

        if (!string.IsNullOrEmpty(CurrentUserId))
        {
            Posts = await _postService.GetFeed(cancellationToken);
        }
    }

    public async Task<IActionResult> OnPostCreatePostAsync(CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            ModelState.AddModelError(string.Empty, "Erro ao recuperar o ID do usuário.");
            return Page();
        }

        PostModel.UserId = userId;

        var post = _mapper.Map<Post>(PostModel);

        var result = await _postService.CreatePost(post, cancellationToken);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, "Não foi possível publicar.");
            return Page();
        }

        TempData["SuccessMessage"] = "Publicação criada com sucesso!";
        return RedirectToPage("/Dashboard");
    }

    public async Task<IActionResult> OnPostDeletePostAsync(long postId, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return new JsonResult(new { success = false, message = "Usuário não autenticado." });
        }


        var result = await _postService.DeletePost( postId, cancellationToken);

        if (!result.IsSuccess)
        {
            return new JsonResult(new { success = false, message = "Não foi possível excluir o post." });
        }

        TempData["SuccessMessage"] = "Post excluído com sucesso!";
        return RedirectToPage("/Dashboard");
    }
    public async Task<IActionResult> OnPostLikePostAsync(long postId, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return new JsonResult(new { success = false, message = "Usuário não autenticado." });
        }
       
        var userIdLong = long.Parse(userId);

        var result = await _likeService.LikeOrDeslike(userIdLong, postId, cancellationToken);

        if (!result.IsSuccess)
        {
            return new JsonResult(new { success = false, message = "Não foi possível curtir o post." });
        }

        return RedirectToPage("/Dashboard");
    }

    //public async Task<IActionResult> OnPostCommentOnPostAsync(long postId, string comment, CancellationToken cancellationToken)
    //{
    //    var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

    //    if (string.IsNullOrEmpty(userId))
    //    {
    //        return new JsonResult(new { success = false, message = "Usuário não autenticado." });
    //    }

    //    var result = await _postService.CommentOnPost(postId, userId, comment, cancellationToken);

    //    if (!result.IsSuccess)
    //    {
    //        return new JsonResult(new { success = false, message = "Não foi possível comentar no post." });
    //    }

    //    return new JsonResult(new { success = true });
    //}
}