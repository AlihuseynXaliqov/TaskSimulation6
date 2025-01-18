using AutoMapper;
using TaskSimulation6.Areas.Manage.ViewModel.Agent;
using TaskSimulation6.Areas.Manage.ViewModel.Position;
using TaskSimulation6.Models;

namespace TaskSimulation6.Areas.Manage.Helpers.Mapper
{
    public class AgentProfile: Profile
    {
        public AgentProfile()
        {
            CreateMap<CreateAgentVm, Agent>().ReverseMap();
            CreateMap<UpdateAgentVM, Agent>().ReverseMap();

        }
    }
}
