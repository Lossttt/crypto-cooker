using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto_app.Config.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type callingType)
        {
            // Hier kunnen services en repositories worden toegevoegd aan de service container
            // zoals bijvoorbeeld:
            // services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}