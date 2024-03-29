using Ninject.Modules;

namespace NinjectInMvvmCrossNet
{
    public abstract class NinjectDependenciesProvider
    {
        /// <summary>
        /// Add platform specific INinjectModule here.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<INinjectModule> GetPlatformSpecificModules();

        /// <summary>
        /// Add your portable Ninject modules here.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<INinjectModule> GetPortableNinjectModules();

        public IEnumerable<INinjectModule> GetNinjectModules()
        {
            foreach (var portableModule in GetPortableNinjectModules())
                yield return portableModule;

            foreach (var platformModule in GetPlatformSpecificModules())
                yield return platformModule;
        }
    }
}
