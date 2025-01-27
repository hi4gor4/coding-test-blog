using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

[Authorize]
public class DashboardModel : PageModel
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string BirthDate { get; private set; }
    public string Phone { get; private set; }

    public void OnGet()
    {
        var claims = User.Claims;

        FullName = claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
        Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        BirthDate = claims.FirstOrDefault(c => c.Type == "BirthDate")?.Value;
        Phone = claims.FirstOrDefault(c => c.Type == "Phone")?.Value;
    }

    public async Task<IActionResult> OnPostCreatePostAsync(PostModel postModel)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Obter UserId da claim
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // ou a chave da claim que voc� usa para o ID do usu�rio

        // Criar o post com o conte�do e o userId
        postModel.UserId = userId;

        // Salvar a publica��o (chama o servi�o para persistir)
        //await _postService.CreatePostAsync(postModel);

        // Redirecionar ou mostrar uma mensagem de sucesso
        TempData["SuccessMessage"] = "Publica��o criada com sucesso!";
        return RedirectToPage("/Dashboard");
    }
}
