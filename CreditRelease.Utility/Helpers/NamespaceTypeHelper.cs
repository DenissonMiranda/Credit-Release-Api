using System.Reflection;

namespace CreditRelease.Utility.Helpers
{
    public static class NamespaceTypeHelper
    {
        public struct NamespaceInfo
        {
            public string Namespace { get; }
            public string Assembly { get; }

            public NamespaceInfo(string assembly, string ns)
            {
                Assembly = assembly;
                Namespace = ns;
            }
        }
        public static IEnumerable<Type> ObtemTypesQueFacamParteDoNamespaceInformado(NamespaceInfo namespaceInfo)
        {

            var tiposDoAssembly = Assembly.Load(namespaceInfo.Assembly).GetTypes();
            var tiposFiltrados = tiposDoAssembly.Where(t => t.TypePertenceAUmaClasseDeMapeamento(namespaceInfo.Namespace));
            var types = tiposFiltrados.ToList();
            return types;
        }

        private static bool TypePertenceAUmaClasseDeMapeamento(this Type t, string namespaceMapeamento)
        {
            return !string.IsNullOrEmpty(t.Namespace) &&
                    t.Namespace.Contains(namespaceMapeamento) &&
                    t.IsClass &&
                    t.IsPublic &&
                    !t.IsAbstract &&
                    !t.IsNested &&
                    t.Name != "<>c";
        }
    }
}
