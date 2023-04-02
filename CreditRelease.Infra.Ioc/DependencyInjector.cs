using CreditRelease.Utility.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CreditRelease.Infra.Ioc
{
    public class DependencyInjector
    {
        public static void Registrar(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var domain = AppDomain.CurrentDomain;

            var namespaces = new List<NamespaceTypeHelper.NamespaceInfo>()
            {
                new NamespaceTypeHelper.NamespaceInfo("CreditRelease.Infra.Data","CreditRelease.Infra.Data.Repository"),
                new NamespaceTypeHelper.NamespaceInfo("CreditRelease.Application","CreditRelease.Application.Services"),
           };

            foreach (var ns in namespaces)
            {
                var listTypes = NamespaceTypeHelper
                    .ObtemTypesQueFacamParteDoNamespaceInformado(ns)
                    .Where(t => t.GetInterfaces().Any()).ToList();
                foreach (var typeImplementation in listTypes)
                {
                    var interfaceType = typeImplementation.GetInterface($"I{typeImplementation.Name}");
                    if (interfaceType != null)
                        serviceCollection.AddScoped(interfaceType, typeImplementation);
                }
            }
             
        }

        private static void CreateSigletonFromConfigurationSection<TSectionConfig>(IServiceCollection serviceCollection, IConfiguration configuration, string nameSection) where TSectionConfig : class
        {
            var sectionConfigClass = Activator.CreateInstance<TSectionConfig>();
            configuration.GetSection(nameSection).Bind(sectionConfigClass);
            serviceCollection.AddSingleton(sectionConfigClass);
        }
    }
}
