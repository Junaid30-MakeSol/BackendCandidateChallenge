using AutoMapper;
using Quiz.BAL.Interfaces;
using Quiz.BAL.Models;
using Quiz.BAL.Models.Domain;
using Quiz.DB.Interfaces;


namespace Quiz.BAL.Managers
{
    public class QuizManager : IQuizManager
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;
        public QuizManager(IQuizRepository quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public List<QuizResponseDataModel> GetAllQuizes()
        {
            var data = _quizRepository.GetAllQuizes();
            var result = _mapper.Map<List<QuizResponseDataModel>>(data);
            return result;
        }


        public QuizResponseDataModel GetQuestionandAnswerByQuizId(int id, QuizModel quiz)
        {
            var questions = _quizRepository.GetQuestionOnQuizId(id);
            var answers = _quizRepository.GetAnswerOnQuizId(id);

            var result = new QuizResponseDataModel
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Questions = questions.Select(question => new QuizResponseDataModel.QuestionItem
                {
                    Id = question.Id,
                    Text = question.Text,
                    Answers = answers.ContainsKey(question.Id)
                        ? answers[question.Id].Select(answer => new QuizResponseDataModel.AnswerItem
                        {
                            Id = answer.Id,
                            Text = answer.Text
                        })
                        : new QuizResponseDataModel.AnswerItem[0],
                    CorrectAnswerId = question.CorrectAnswerId
                }),
                Links = new Dictionary<string, string>
                {
                    {"self", $"/api/quizzes/{id}"},
                    {"questions", $"/api/quizzes/{id}/questions"}
                }
            };

            return result;


        }


        public QuizModel GetQuiz(int id)
        {
            var data = _quizRepository.GetQuiz(id);
            var result = _mapper.Map<QuizModel>(data);
            return result;
        }

    }
}
