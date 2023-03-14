using AutoMapper;
using OnlineTest.Model;
using OnlineTest.Model.Interface;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;

namespace OnlineTest.Service.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;
        public QuestionService(IQuestionRepository questionRepository, ITestRepository testRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _testRepository = testRepository;
            _mapper = mapper;
        }
        public ResponseDTO AddQuestion(QuestionDTO questionDTO)
        {
            try
            {
                if (_testRepository.GetTestById(questionDTO.TestId) == null)
                    return new ResponseDTO
                    {
                        Status = 200,
                        Message = "TestId is not exits.",
                    };
                var flag = _questionRepository.AddQuestion(_mapper.Map<Question>(questionDTO));
                if (flag > 0)
                    return new ResponseDTO
                    {
                        Status = 200,
                        Data = new
                        {
                            QuestionId = flag,
                        },
                        Message = "Question added successfully.",
                    };
                else
                    return new ResponseDTO
                    {
                        Status = 200,
                        Message = "Question not added.",
                    };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }

        }

        public ResponseDTO GetById(int Id)
        {
            try
            {
                var que = _mapper.Map<QuestionDTO>(_questionRepository.GetById(Id));
                if (que == null)
                    return new ResponseDTO
                    {
                        Status = 400,
                        Message = "No Question found.",
                    };
                else
                    return new ResponseDTO
                    {
                        Status = 200,
                        Data = que,
                        Message = "All Question.",
                    };

            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }

        public ResponseDTO GetQuesByTestId(int testId)
        {
            try
            {
                var que = _mapper.Map<List<QuestionDTO>>(_questionRepository.GetQuesByTestId(testId).ToList());
                if (que.Count == 0)
                    return new ResponseDTO
                    {
                        Status = 400,
                        Message = "No Question found.",
                    };
                else
                    return new ResponseDTO
                    {
                        Status = 200,
                        Data = que,
                        Message = "All Question.",
                    };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }

        public ResponseDTO UpdateQuestion(QuestionDTO questionDTO)
        {
            try
            {
                if (_questionRepository.UpdateQuestion(_mapper.Map<Question>(questionDTO)))
                    return new ResponseDTO
                    {
                        Status = 200,
                        Message = "Question updated successfully."
                    };
                else
                    return new ResponseDTO
                    {
                        Status = 400,
                        Message = "Question not updated.",
                    };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }
    }
}