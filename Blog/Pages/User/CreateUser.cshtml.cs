using AutoMapper;
using Core.Entities.Users;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;
using System.Threading.Tasks;

public class CreateUserModel : PageModel
{
    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public string Surname { get; set; }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Document { get; set; }

    [BindProperty]
    public string Phone { get; set; }
    [BindProperty]
    public DateTime BirthDate { get; set; }

    [BindProperty]
    public string Password { get; set; }

    [BindProperty]
    public string ConfirmPassword { get; set; }

    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public CreateUserModel(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = _mapper.Map<User>(this);

        var result = await _userService.AddAsync(user, this.ConfirmPassword, cancellationToken);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.Errors.First());
            return Page();
        }

        TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";
        return RedirectToPage("/Index"); 
    }
}
