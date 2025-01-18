using FluentValidation;

namespace TaskSimulation6.Areas.Manage.ViewModel.Agent
{
    public class UpdateAgentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int PositionId { get; set; }
        public IFormFile? formFile { get; set; }
    }
    public class UpdateAgentValidator : AbstractValidator<UpdateAgentVM>
    {
        public UpdateAgentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull().WithMessage("Ad bos ola bilmez")
                .MinimumLength(4).WithMessage("Adin uzunluqu minimum 4 olmalidi");
            RuleFor(x => x.PositionId)
                .NotEmpty()
                .NotNull().WithMessage("Position bos ola bilmez");
          
        }
    }
}