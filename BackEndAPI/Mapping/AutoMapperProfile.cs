using AutoMapper;
using BackEndAPI.Contracts;
using BackEndAPI.Models;

namespace BackEndAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TaskDTO, ToDo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
            CreateMap<GoalDTO, Goal>()
                .ForMember(dest => dest.GoalId, opt => opt.Ignore());
        }
    }
}