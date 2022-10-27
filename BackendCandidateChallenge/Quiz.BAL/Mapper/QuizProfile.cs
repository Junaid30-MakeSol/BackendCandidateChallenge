using AutoMapper;
using Quiz.BAL.Models;
using Quiz.BAL.Models.Domain;
using Quiz.DB.Models;
using Quiz.DB.Models.Domain;

namespace Quiz.BAL.Mapper
{
    public class QuizProfile:Profile
    {
        public QuizProfile()
        {
            CreateMap<QuizResponseDataModel, QuizResponseDto>().ReverseMap();
            CreateMap<QuizModel, QuizDto>().ReverseMap();
        }
    }
}
