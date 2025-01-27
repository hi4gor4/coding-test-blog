using Core.Entities.Users;
using FluentValidation;

namespace Core.Validators;

public class CreateUserValidator : AbstractValidator<User>
{

    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O campo Nome é obrigatório.")
            .MaximumLength(100).WithMessage("O Nome não pode ter mais de 100 caracteres.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("O campo Sobrenome é obrigatório.")
            .MaximumLength(100).WithMessage("O Sobrenome não pode ter mais de 100 caracteres.");

        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("O campo Documento é obrigatório.")
            .Length(11).WithMessage("O Documento deve ter 11 caracteres.")
            .Matches("^[0-9]*$").WithMessage("O Documento deve conter apenas números.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("O campo Senha é obrigatório.")
            .MinimumLength(8).WithMessage("A Senha deve ter pelo menos 8 caracteres.")
            .Matches("[A-Z]").WithMessage("A Senha deve conter pelo menos uma letra maiúscula.")
            .Matches("[a-z]").WithMessage("A Senha deve conter pelo menos uma letra minúscula.")
            .Matches("[0-9]").WithMessage("A Senha deve conter pelo menos um número.")
            .Matches("[^a-zA-Z0-9]").WithMessage("A Senha deve conter pelo menos um caractere especial.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O campo E-mail é obrigatório.")
            .EmailAddress().WithMessage("O E-mail deve ser válido.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("O campo Telefone é obrigatório.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("O Telefone deve estar em um formato internacional válido.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("O campo Data de Nascimento é obrigatório.")
            .Must(BeAValidAge).WithMessage("A Data de Nascimento deve indicar pelo menos 18 anos.");
    }

    private bool BeAValidAge(DateTime bithDate)
    {
        var age = DateTime.Now.Year - bithDate.Year;
        if (bithDate > DateTime.Now.AddYears(-age)) age--;
        return age >= 18;
    }
}
