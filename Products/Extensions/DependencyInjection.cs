using Api.Middlewares;

namespace Api.Extensions
{
    public static class DependencyInjection
    {
        public static void AddPresentation(this IServiceCollection services)
        {
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                // Configure JSON serializer settings to keep the Original names in serialization and deserialization
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<ErrorHandlingMiddleware>();

            services.AddProblemDetails();

        }
    }
}
