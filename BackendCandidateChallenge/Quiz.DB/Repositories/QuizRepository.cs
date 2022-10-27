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

        public IEnumerable<QuizResponseDto> GetAllQuizes()
        {
            var sql = "SELECT * FROM Quiz;";
            var quizzes = _connection.Query<QuizDto>(sql);
            return quizzes.Select(quiz =>
                new QuizResponseDto
                {
                    Id = quiz.Id,
                    Title = quiz.Title
                });
        }
       
        public QuizDto GetQuiz(int id)
        {
            var quizSql = "SELECT * FROM Quiz WHERE Id = @Id;";
            var quiz = _connection.QuerySingle<QuizDto>(quizSql, new { Id = id });
            return quiz;
        }
        public IEnumerable<QuestionDto> GetQuestionOnQuizId(int id)
        {
            var questionsSql = "SELECT * FROM Question WHERE QuizId = @QuizId;";
            var questions = _connection.Query<QuestionDto>(questionsSql, new { QuizId = id });
            return questions;
        }

        public Dictionary<int, IList<AnswerDto>> GetAnswerOnQuizId(int id)
        {
            var answersSql = "SELECT a.Id, a.Text, a.QuestionId FROM Answer a INNER JOIN Question q ON a.QuestionId = q.Id WHERE q.QuizId = @QuizId;";

            var answers = _connection.Query<AnswerDto>(answersSql, new { QuizId = id })
            .Aggregate(new Dictionary<int, IList<AnswerDto>>(), (dict, answer) => {
                if (!dict.ContainsKey(answer.QuestionId))
                    dict.Add(answer.QuestionId, new List<AnswerDto>());
                dict[answer.QuestionId].Add(answer);
                return dict;
            });
            return answers;
        }


    }
}
