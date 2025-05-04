using AutoMapper;
using BLL.Models;
using Domain.Entities;

namespace BLL.MappingProfiles
{
    //Клас для налаштування мапінгу між сутностями Domain та моделями BLL
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Мапінг між User та UserModel
            CreateMap<User, UserModel>().ReverseMap();

            //Мапінг між Place та PlaceModel
            CreateMap<Place, PlaceModel>().ReverseMap();

            //Мапінг між MediaFile та MediaFileModel
            CreateMap<MediaFile, MediaFileModel>().ReverseMap();

            //Мапінг між Question та QuestionModel
            CreateMap<Question, QuestionModel>().ReverseMap();

            //Мапінг між Review та ReviewModel
            CreateMap<Review, ReviewModel>().ReverseMap();

            //Мапінг між Visit та VisitModel
            CreateMap<Visit, VisitModel>().ReverseMap();
        }
    }
}
