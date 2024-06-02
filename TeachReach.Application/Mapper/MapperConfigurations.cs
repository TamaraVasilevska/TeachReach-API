using AutoMapper;
using TeachReach.TeachReach.Application.RequestModels.ReviewRequestModels;
using TeachReach.TeachReach.Application.RequestModels.SessionRequestModels;
using TeachReach.TeachReach.Application.RequestModels.StudentRequestModels;
using TeachReach.TeachReach.Application.RequestModels.TeacherRequestModels;
using TeachReach.TeachReach.Domain.Entities;
namespace TeachReach.TeachReach.Application.Mapper
{
    public class MapperConfigurations
    {
        public static IMapper CreateMapper()
        {
            MapperConfiguration configuration = new MapperConfiguration(config =>
            {
                config.CreateMap<ReviewRequestDto, Review>();
                config.CreateMap<SessionRequestDto, Session>();
                config.CreateMap<StudentLoginDto, Student>();
                config.CreateMap<TeacherLoginDto, Teacher>();
                config.CreateMap<TeacherRegisterDto, Teacher>();
            });
            return configuration.CreateMapper();
        }
     }
}
