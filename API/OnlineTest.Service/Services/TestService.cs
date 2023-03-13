using AutoMapper;
using OnlineTest.Model;
using OnlineTest.Model.Interface;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;

namespace OnlineTest.Service.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;
        public TestService(ITestRepository testRepository,IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }

        public bool AddTest(AddTestDTO addTestDTO)
        {
            var test = _mapper.Map<Test>(addTestDTO);
            test.CreatedTime= DateTime.Now;
            return _testRepository.AddTest(test);
        }

        public bool DeleteTest(TestDTO testDTO)
        {
            return _testRepository.DeleteTest(_mapper.Map<Test>(testDTO));
        }

        public List<TestDTO> GetAllTest(int page, int? limit = null)
        {
            var test = new List<TestDTO>();
            _testRepository.GetTests(page, limit)
                .ToList()
                .ForEach(item => test.Add(_mapper.Map<TestDTO>(item)));
            return test;
        }

        public TestDTO GetTesyById(int id)
        {
            var data = _testRepository.GetTestById(id);
            return _mapper.Map<TestDTO>(data);
        }

        public bool UpdateTest(TestDTO testDTO)
        {
            return _testRepository.UpdateTest(_mapper.Map<Test>(testDTO));
        }
    }
}
