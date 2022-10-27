using Quiz.DB.Models;
using Quiz.DB.Models.Domain;
namespace Quiz.DB.Interfaces
{
    public interface IQuizRepository
    {
        QuizModel GetQuiz(int id);
        IEnumerable<QuizResponseDataModel> GetAllQuizes();
        IEnumerable<QuestionModel> GetQuestionOnQuizId(int id);
        Dictionary<int, IList<AnswerModel>> GetAnswerOnQuizId(int id);
    }
}
