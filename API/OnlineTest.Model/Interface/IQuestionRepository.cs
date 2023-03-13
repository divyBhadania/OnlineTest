namespace OnlineTest.Model.Interface
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetQuesByTestId(int testId);
        Question GetById(int Id);
        bool AddQuestion(Question question);
        bool UpdateQuestion(Question question);

    }
}
