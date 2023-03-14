using OnlineTest.Service.DTO;

namespace OnlineTest.Service.Interface
{
    public interface ITestService
    {
        ResponseDTO GetAllTest(int page, int? limit = null);
        ResponseDTO GetTestById(int id);
        ResponseDTO AddTest(int id ,AddTestDTO addTestDTO);
        bool DeleteTest(TestDTO testDTO);
        ResponseDTO UpdateTest(UpdateTestDTO testDTO);
    }
}
