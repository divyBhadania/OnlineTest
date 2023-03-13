using OnlineTest.Service.DTO;

namespace OnlineTest.Service.Interface
{
    public interface ITestService
    {
        List<TestDTO> GetAllTest(int page, int? limit = null);
        TestDTO GetTesyById(int id);
        bool AddTest(AddTestDTO addTestDTO);
        bool DeleteTest(TestDTO testDTO);
        bool UpdateTest(TestDTO testDTO);
    }
}
