using Quiz.BAL.Models;
using Quiz.BAL.Models.Domain;

namespace Quiz.BAL.Interfaces
{
    public interface IQuizManager
    {
        IEnumerable<QuizResponseDataModel> GetAllQuizes();
        IEnumerable<QuestionModel> GetQuestionOnQuizId(int id);
        Dictionary<int, IList<AnswerModel>> GetAnswerOnQuizId(int id);
        QuizModel GetQuiz(int id);
    }
}
