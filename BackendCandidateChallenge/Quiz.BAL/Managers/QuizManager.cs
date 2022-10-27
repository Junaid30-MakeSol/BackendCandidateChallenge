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

        public IEnumerable<QuizResponseDataModel> GetAllQuizes()
        {
            var data = _quizRepository.GetAllQuizes();
            var result = _mapper.Map<IEnumerable<QuizResponseDataModel>>(data);
            return result;
        }

        public Dictionary<int, IList<AnswerModel>> GetAnswerOnQuizId(int id)
        {
            var data = _quizRepository.GetAnswerOnQuizId(id);
            var result = _mapper.Map<Dictionary<int, IList<AnswerModel>>>(data);
            return result;
        }

        public IEnumerable<QuestionModel> GetQuestionOnQuizId(int id)
        {
            var data = _quizRepository.GetQuestionOnQuizId(id);
            var result = _mapper.Map<IEnumerable<QuestionModel>>(data);
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
