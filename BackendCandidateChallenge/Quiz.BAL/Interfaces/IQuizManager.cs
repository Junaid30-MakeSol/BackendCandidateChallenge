using Quiz.BAL.Models;
using Quiz.BAL.Models.Domain;

namespace Quiz.BAL.Interfaces
{
    public interface IQuizManager
    {
        List<QuizResponseDataModel> GetAllQuizes();
        QuizModel GetQuiz(int id);
        QuizResponseDataModel GetQuestionandAnswerByQuizId(int id, QuizModel quiz);
    }
}
