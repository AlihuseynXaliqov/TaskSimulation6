using FluentValidation;

namespace TaskSimulation6.Areas.Manage.ViewModel.Position
{
    public class UpdatePositionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdatePositionValidator : AbstractValidator<UpdatePositionVM>
    {
        public UpdatePositionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull().WithMessage("Ad bos ola bilmez")
                .MinimumLength(4).WithMessage("Adin uzunluqu minimum 4 olmalidi");
        }
    }
}

