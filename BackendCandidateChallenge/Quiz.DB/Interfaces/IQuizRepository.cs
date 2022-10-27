using Quiz.DB.Models;
using Quiz.DB.Models.Domain;
namespace Quiz.DB.Interfaces
{
    public interface IQuizRepository
    {
        QuizDto GetQuiz(int id);
        List<QuizResponseDto> GetAllQuizes();
        List<QuestionDto> GetQuestionOnQuizId(int id);
        Dictionary<int, IList<AnswerDto>> GetAnswerOnQuizId(int id);
    }
}
