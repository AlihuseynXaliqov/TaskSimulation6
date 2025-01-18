using FluentValidation;

namespace TaskSimulation6.ViewModel.Account
{
    public class RegisterVm
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
    public class RegisterValidation : AbstractValidator<RegisterVm>
    {
        public RegisterValidation()
        {
            RuleFor(x=>x.Name).NotEmpty().NotNull().WithMessage("bos Ola bilmez").MinimumLength(3).WithMessage("name uzunlugu minimum 3 ola biler");
            RuleFor(x=>x.UserName).NotEmpty().NotNull().WithMessage("bos Ola bilmez").MinimumLength(3).WithMessage("name uzunlugu minimum 3 ola biler");
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("bos Ola bilmez").EmailAddress();

            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("bos Ola bilmez")
                .Matches("[A-Z]").WithMessage("Sifrede en az bir boyuk herf olmalidi")
                .Matches("[a-z]").WithMessage("Sifrede en az bir kicik herf olmalidi")
                .Matches("[0-9]").WithMessage("Sifrede en az bir reqem olmalidi")
                .Matches("[^A-Za-z0-9]").WithMessage("Sifrede en az bir qeribe simvol olmalidi")
                .MinimumLength(8).WithMessage("sifre min 8 olmalidi");
            RuleFor(x => x).Must(x => x.Password == x.ConfirmPassword).WithMessage("Parollar eyni deyil").NotEmpty().NotNull().WithMessage("bos Ola bilmez");

        }
    }
}
