using System.Reflection;
using crypto_app.Config.Installers;

namespace crypto_app.Config.Extensions
{
    public static class InstallerExtension
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration, Type callingType)
        {
            var installers = Assembly.GetExecutingAssembly().ExportedTypes
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration, callingType));
        }
    }
}