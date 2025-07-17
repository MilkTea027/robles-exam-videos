using BFF.Videos.Repositories;
using BFF.Videos.Repositories.Interfaces;
using BFF.Videos.Services;
using BFF.Videos.Services.Interfaces;

namespace BFF.Videos.Configurations
{
    public static class InjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // REPOSITORIES
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IVideoRepository, VideoRepository>();

            // SERVICES
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IVideoService, VideoService>();
        }
    }
}