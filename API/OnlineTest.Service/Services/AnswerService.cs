using AutoMapper;
using OnlineTest.Model.Interface;
using OnlineTest.Service.Interface;
using System.Runtime.CompilerServices;

namespace OnlineTest.Service.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        public AnswerService(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }
    }
}
