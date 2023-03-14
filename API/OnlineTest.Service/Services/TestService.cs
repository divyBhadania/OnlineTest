using AutoMapper;
using Azure;
using OnlineTest.Model;
using OnlineTest.Model.Interface;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;
using System.Collections.Generic;

namespace OnlineTest.Service.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        public TestService(ITestRepository testRepository, IQuestionRepository questionRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public ResponseDTO GetAllTest(int page, int? limit = null)
        {
            try
            {
                var tests = _mapper.Map<List<TestDTO>>(_testRepository.GetTests(page, limit).ToList());
                return new ResponseDTO
                {
                    Status = 200,
                    Data = tests,
                    Message = "All test"
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

        public ResponseDTO AddTest(int id, AddTestDTO addTestDTO)
        {
            try
            {
                var test = _mapper.Map<Test>(addTestDTO);
                test.CreatedTime = DateTime.Now;
                test.CreatedBy = id;
                var flag = _testRepository.AddTest(test);
                if (flag > 0)
                    return new ResponseDTO
                    {
                        Status = 200,
                        Data = new
                        {
                            TestId = id,
                        },
                        Message = "Test added successfully",
                    };
                else
                    return new ResponseDTO
                    {
                        Status = 400,
                        Message = "Technology Not added.",
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

        public bool DeleteTest(TestDTO testDTO)
        {
            return _testRepository.DeleteTest(_mapper.Map<Test>(testDTO));
        }

        public ResponseDTO GetTestById(int id)
        {
            try
            {
                var test = _mapper.Map<TestDTO>(_testRepository.GetTestById(id));
                if (test != null)
                {
                    var ques = _mapper.Map<List<QuestionDTO>>(_questionRepository.GetQuesByTestId(id).ToList());
                    var QAlist = new List<object>();
                    foreach (var item in ques)
                    {
                        QAlist.Add(new
                        {
                            id = item.Id,
                            questionName = item.QuestionName,
                            que = item.Que,
                            type = item.Type,
                            weightage = item.Weightage,
                            answers = new AnswerDTO()
                        });
                    }
                    return new ResponseDTO
                    {
                        Status = 200,
                        Data = new
                        {
                            id = test.Id,
                            testName = test.TestName,
                            description = test.Description,
                            expireOn = test.ExpireOn,
                            technologyId = test.TechnologyId,
                            questions = QAlist
                        }
                    };
                }
                else
                    return new ResponseDTO
                    {
                        Status = 404,
                        Message = "Not valid Test Id"
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

        public ResponseDTO UpdateTest(UpdateTestDTO testDTO)
        {
            try
            {
                var oldTest = _testRepository.GetTestById(testDTO.Id);
                if (oldTest == null)
                    return new ResponseDTO
                    {
                        Status = 404,
                        Message = "Not valid Test Id"
                    };
                var newTest = _mapper.Map<Test>(testDTO);
                newTest.CreatedTime = oldTest.CreatedTime;
                newTest.CreatedBy = oldTest.CreatedBy;
                if (_testRepository.UpdateTest(newTest))
                    return new ResponseDTO
                    {
                        Status = 200,
                        Message = "Test Updated sucessfully",
                    };
                else
                    return new ResponseDTO
                    {
                        Status = 400,
                        Message = "Test not Updated sucessfully",
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
