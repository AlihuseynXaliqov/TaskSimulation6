using TaskSimulation6.Models.Base;

namespace TaskSimulation6.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Agent> Agents { get; set; }
    }
}
