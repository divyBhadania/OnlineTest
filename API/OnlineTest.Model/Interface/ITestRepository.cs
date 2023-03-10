namespace OnlineTest.Model.Interface
{
    public interface ITestRepository
    {
        IEnumerable<Test> GetTests(int page, int? limit = null);
        Test GetTestById(int id);
        int AddTest(Test test);
        bool UpdateTest(Test test);
        bool DeleteTest(Test test);
    }
}
