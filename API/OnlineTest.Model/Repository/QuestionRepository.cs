using OnlineTest.Model.Interface;

namespace OnlineTest.Model.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        public bool AddQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public Question GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetQuesByTestId(int testId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateQuestion(Question question)
        {
            throw new NotImplementedException();
        }
    }
}
