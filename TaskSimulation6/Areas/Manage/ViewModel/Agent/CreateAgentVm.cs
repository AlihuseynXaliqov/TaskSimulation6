using FluentValidation;
using TaskSimulation6.Areas.Manage.ViewModel.Position;

namespace TaskSimulation6.Areas.Manage.ViewModel.Agent
{
    public class CreateAgentVm
    {
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public int? PositionId { get; set; }
        public IFormFile? formFile { get; set; }
    }
    public class CreateAgentValidator : AbstractValidator<CreateAgentVm>
    {
        public CreateAgentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull().WithMessage("Ad bos ola bilmez")
                .MinimumLength(4).WithMessage("Adin uzunluqu minimum 4 olmalidi");
            RuleFor(x => x.PositionId)
                .NotEmpty()
                .NotNull().WithMessage("Position bos ola bilmez");
            RuleFor(x => x.formFile)
                 .NotEmpty()
                .NotNull().WithMessage("Image bos ola bilmez");
        }
    }
}
