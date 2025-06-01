using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.UnitOfWork;
using BLL.Interfaces;
using BLL.Services;
using AutoMapper;
using BLL.MappingProfiles;
using API.Mapping;

namespace API.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, string connectionString)
        {
            // Реєстрація контексту бази даних
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

            // Реєстрація UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Реєстрація сервісів бізнес-логіки
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IVisitService, VisitService>();
            services.AddScoped<IMediaFileService, MediaFileService>();

            // Реєстрація AutoMapper з профілями з BLL і API
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();     // BLL: Entity ↔ Model
                cfg.AddProfile<ApiMappingProfile>();  // API: Model ↔ DTO
            });

            return services;
        }
    }
}
