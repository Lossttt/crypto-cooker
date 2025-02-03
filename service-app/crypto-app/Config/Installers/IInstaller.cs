using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto_app.Config.Installers
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration, Type callingType);
    }
}