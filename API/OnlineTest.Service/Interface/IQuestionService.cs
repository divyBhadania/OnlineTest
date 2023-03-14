using OnlineTest.Model;
using OnlineTest.Service.DTO;

namespace OnlineTest.Service.Interface
{
    public interface IQuestionService
    {
        ResponseDTO GetQuesByTestId(int testId);
        ResponseDTO GetById(int Id);
        ResponseDTO AddQuestion(QuestionDTO questionDTO);
        ResponseDTO UpdateQuestion(QuestionDTO questionDTO);
    }
}
