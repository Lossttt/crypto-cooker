using crypto_app.Config.Options;
using crypto_app.Infrastructure.Repositories;
using crypto_app.Infrastructure.Repositories.Interfaces;
using crypto_app.Services;
using crypto_app.Services.Interfaces;

namespace crypto_app.Config.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type callingType)
        {
            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddScoped<ITokenFactory, TokenFactory>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();

            // Options
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        }
    }
}