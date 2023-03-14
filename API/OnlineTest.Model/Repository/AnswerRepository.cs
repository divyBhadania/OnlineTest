using OnlineTest.Model.Interface;

namespace OnlineTest.Model.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly OnlineTestContext _context;
        public AnswerRepository(OnlineTestContext context)
        {
            _context = context;
        }

    }
}
