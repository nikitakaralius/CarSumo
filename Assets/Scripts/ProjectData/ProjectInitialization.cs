using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Initialization;
using Zenject;

namespace Infrastructure
{
    public class ProjectInitialization
    {
        private readonly DiContainer _container;

        public ProjectInitialization(DiContainer container)
        {
            _container = container;
        }

        public async Task InitializeAsync()
        {
            var dataFilesInitialization = _container.Instantiate<DataFilesInitialization>();
            dataFilesInitialization.Initialize();

            IEnumerable<Type> initializationTypes = GetProjectAsyncInitializationsTypes();
            IEnumerable<IAsyncInitializable> initializations = CreateProjectInitializations(initializationTypes);

            await Task.WhenAll(initializations.Select(initialization => initialization.InitializeAsync()));
        }

        private IEnumerable<IAsyncInitializable> CreateProjectInitializations(IEnumerable<Type> types)
        {
            return types.Select(type => (IAsyncInitializable)_container.Instantiate(type));
        }

        private IEnumerable<Type> GetProjectAsyncInitializationsTypes()
        {
            Type initializableBaseType = typeof(IAsyncInitializable);
            
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsAssignableFrom(initializableBaseType) && type.IsClass);
        }
    }
}