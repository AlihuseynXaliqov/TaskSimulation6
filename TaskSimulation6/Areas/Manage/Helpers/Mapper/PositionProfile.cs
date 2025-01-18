using AutoMapper;
using TaskSimulation6.Areas.Manage.ViewModel.Position;
using TaskSimulation6.Models;

namespace TaskSimulation6.Areas.Manage.Helpers.Mapper
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<CreatePositionVm, Position>().ReverseMap();
            CreateMap<UpdatePositionVM, Position>().ReverseMap();

        }
    }
}
