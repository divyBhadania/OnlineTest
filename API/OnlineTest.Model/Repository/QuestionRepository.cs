using OnlineTest.Model.Interface;

namespace OnlineTest.Model.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly OnlineTestContext _context;
        public QuestionRepository(OnlineTestContext context)
        {
            _context = context;
        }
        public int AddQuestion(Question question)
        {
            _context.Questions.Add(question);
            if (_context.SaveChanges() > 0)
                return question.Id;
            else
                return 0;
        }

        public Question GetById(int Id)
        {
            return _context.Questions.Where(i => i.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Question> GetQuesByTestId(int testId)
        {
            return _context.Questions.Where(i => i.TestId == testId);
        }

        public bool UpdateQuestion(Question question)
        {
            _context.Questions.Update(question);
            return _context.SaveChanges() > 0;
        }
    }
}
