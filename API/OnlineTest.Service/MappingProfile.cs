using AutoMapper;
using OnlineTest.Model;
using OnlineTest.Service.DTO;

namespace OnlineTest.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Technology, TechnologyDTO>();
            CreateMap<TechnologyDTO, Technology>();

            CreateMap<AddUserDTO, User>();
            CreateMap<User, GetUserDTO>();
            CreateMap<User, AddUserDTO>();

            CreateMap<TestDTO, Test>();
            CreateMap<AddTestDTO, Test>();
            CreateMap<Test, TestDTO>();
            CreateMap<UpdateTestDTO, Test>();

            CreateMap<QuestionDTO, Question>();
            CreateMap<Question, QuestionDTO>();
        }
    }
}
