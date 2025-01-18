namespace TaskSimulation6.Areas.Manage.Helpers.Exception
{
    public class NegativeIdException:System.Exception
    {
        public NegativeIdException() : base("Id menfi ve sifir ola bilmez") { }
        public NegativeIdException(string message) : base(message) { }

    }
}
