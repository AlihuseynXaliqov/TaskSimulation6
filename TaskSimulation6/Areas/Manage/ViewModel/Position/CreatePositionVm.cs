using FluentValidation;

namespace TaskSimulation6.Areas.Manage.ViewModel.Position
{
    public class CreatePositionVm
    {
        public string Name { get; set; }
    }

    public class CreatePositionValidator : AbstractValidator<CreatePositionVm>
    {
        public CreatePositionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull().WithMessage("Ad bos ola bilmez")
                .MinimumLength(4).WithMessage("Adin uzunluqu minimum 4 olmalidi");
        }
    }
}
