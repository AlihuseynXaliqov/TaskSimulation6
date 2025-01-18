namespace TaskSimulation6.Areas.Manage.Helpers.Exception
{
    public class NotFoundException : System.Exception
    {

        public NotFoundException() : base("Tapilmadi") { }
        public NotFoundException(string message) : base(message) { }

    }
}
