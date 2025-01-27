using AutoMapper;
using Core.Entities.Posts;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

[Authorize]
public class DashboardModel : PageModel
{
    [BindProperty]
    public PostModel PostModel { get; set; }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string BirthDate { get; private set; }
    public string Phone { get; private set; }

    private readonly IPostService _postService;
    private readonly IMapper _mapper;

    public DashboardModel(IPostService postService, IMapper mapper)
    {
        _postService = postService;
        _mapper = mapper;
    }

    public void OnGet()
    {
        var claims = User.Claims;

        FullName = claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
        Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        BirthDate = claims.FirstOrDefault(c => c.Type == "BirthDate")?.Value;
        Phone = claims.FirstOrDefault(c => c.Type == "Phone")?.Value;
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

        var result  = await _postService.CreatePost(post, cancellationToken);

        if(!result.IsSuccess)
            ModelState.AddModelError(string.Empty, "Nçao foi possivel publicar.");


        TempData["SuccessMessage"] = "Publicação criada com sucesso!";
        return RedirectToPage("/Dashboard");
    }
}
