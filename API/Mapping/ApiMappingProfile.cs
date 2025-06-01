using API.DTOs;
using AutoMapper;
using BLL.Models;

namespace API.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            // User ↔ UserDto
            CreateMap<UserModel, UserDto>()
                .ReverseMap();

            // Place ↔ PlaceDto
            CreateMap<PlaceModel, PlaceDto>()
                .ReverseMap();

            // Review ↔ ReviewDto
            CreateMap<ReviewModel, ReviewDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Place, opt => opt.Ignore());

            // Question ↔ QuestionDto
            CreateMap<QuestionModel, QuestionDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Place, opt => opt.Ignore());

            // Visit ↔ VisitDto
            CreateMap<VisitModel, VisitDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Place, opt => opt.Ignore());

            // MediaFile ↔ MediaFileDto
            CreateMap<MediaFileModel, MediaFileDto>()
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place))
                .ReverseMap()
                .ForMember(dest => dest.Place, opt => opt.Ignore());
        }
    }
}
