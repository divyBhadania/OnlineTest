using Azure;
using OnlineTest.Model.Interface;

namespace OnlineTest.Model.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly OnlineTestContext _context;
        public TestRepository(OnlineTestContext context)
        {
            _context = context;
        }
        public bool AddTest(Test test)
        {
            _context.Tests.Add(test);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteTest(Test test)
        {
            _context.Tests.Remove(test);
            return _context.SaveChanges() > 0;
        }

        public Test GetTestById(int id)
        {
            var data = _context.Tests.Where(i => i.Id== id).FirstOrDefault();
            return data == null ? null : data;
        }

        public IEnumerable<Test> GetTests(int page, int? limit = null)
        {
            var data = limit == null ? _context.Tests : _context.Tests.Skip((page - 1) * (int)limit).Take((int)limit);
            return data==null ? null : data;
        }

        public bool UpdateTest(Test test)
        {
            //_context.Tests.Update(test); 
            _context.Entry(test).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return _context.SaveChanges() > 0;
        }
    }
}
