using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto_app.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace crypto_app.Config.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type callingType)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CCDbContext>(options => 
                options.UseSqlite(connectionString)
            );
        }
    }
}