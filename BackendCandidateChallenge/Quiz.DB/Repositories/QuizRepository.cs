using Dapper;
using Quiz.DB.Interfaces;
using Quiz.DB.Models;
using Quiz.DB.Models.Domain;
using System.Data;

namespace Quiz.DB.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly IDbConnection _connection;
        public QuizRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<QuizResponseDataModel> GetAllQuizes()
        {
            var sql = "SELECT * FROM Quiz;";
            var quizzes = _connection.Query<QuizModel>(sql);
            return quizzes.Select(quiz =>
                new QuizResponseDataModel
                {
                    Id = quiz.Id,
                    Title = quiz.Title
                });
        }
       
        public QuizModel GetQuiz(int id)
        {
            var quizSql = "SELECT * FROM Quiz WHERE Id = @Id;";
            var quiz = _connection.QuerySingle<QuizModel>(quizSql, new { Id = id });
            return quiz;
        }
        public IEnumerable<QuestionModel> GetQuestionOnQuizId(int id)
        {
            var questionsSql = "SELECT * FROM Question WHERE QuizId = @QuizId;";
            var questions = _connection.Query<QuestionModel>(questionsSql, new { QuizId = id });
            return questions;
        }

        public Dictionary<int, IList<AnswerModel>> GetAnswerOnQuizId(int id)
        {
            var answersSql = "SELECT a.Id, a.Text, a.QuestionId FROM Answer a INNER JOIN Question q ON a.QuestionId = q.Id WHERE q.QuizId = @QuizId;";

            var answers = _connection.Query<AnswerModel>(answersSql, new { QuizId = id })
            .Aggregate(new Dictionary<int, IList<AnswerModel>>(), (dict, answer) => {
                if (!dict.ContainsKey(answer.QuestionId))
                    dict.Add(answer.QuestionId, new List<AnswerModel>());
                dict[answer.QuestionId].Add(answer);
                return dict;
            });
            return answers;
        }


    }
}
