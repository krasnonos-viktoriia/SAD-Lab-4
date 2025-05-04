using AutoMapper;
using BLL.Models;
using Domain.Entities;

namespace BLL.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserModel>().ReverseMap();

            // Place
            CreateMap<Place, PlaceModel>().ReverseMap();

            // MediaFile
            CreateMap<MediaFile, MediaFileModel>().ReverseMap();

            // Question
            CreateMap<Question, QuestionModel>().ReverseMap();

            // Review
            CreateMap<Review, ReviewModel>().ReverseMap();

            // Visit
            CreateMap<Visit, VisitModel>().ReverseMap();
        }
    }
}
