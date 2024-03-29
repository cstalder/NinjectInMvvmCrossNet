using MvvmCross.Base;
using MvvmCross.IoC;
using Ninject;
using Ninject.Modules;

namespace NinjectInMvvmCrossNet
{
    public class NinjectMvxIoCProvider : MvxSingleton<IMvxIoCProvider>, IMvxIoCProvider
    {
        public readonly StandardKernel Kernel;
        private readonly Queue<ActionContainer> _delayedCallbacks = new Queue<ActionContainer>();

        // Class to hold type and action for delayed callbacks
        class ActionContainer
        {
            public Type? Type { get; set; }
            public Action? Action { get; set; }
        }

        // Constructor with modules
        public NinjectMvxIoCProvider(params INinjectModule[] modules)
        {
            Kernel = new StandardKernel(modules);
        }

        // Constructor with settings and modules
        public NinjectMvxIoCProvider(INinjectSettings settings, params INinjectModule[] modules)
        {
            Kernel = new StandardKernel(settings, modules);
        }

        // Queue a callback to be executed when a type is registered
        public void CallbackWhenRegistered(Type type, Action action)
        {
            _delayedCallbacks.Enqueue(new ActionContainer() { Action = action, Type = type });
        }

        // Queue a callback to be executed when a type is registered
        public void CallbackWhenRegistered<T>(Action action)
        {
            CallbackWhenRegistered(typeof(T), action);
        }

        // Check if a type can be resolved
        public bool CanResolve(Type type)
        {
            return (bool)Kernel.CanResolve(type);
        }

        // Check if a type can be resolved
        public bool CanResolve<T>() where T : class => Kernel.CanResolve<T>();

        // Create an instance of a type
        public object Create(Type type) => Kernel.Get(type);

        // Create an instance of a type
        public T Create<T>() where T : class => Kernel.Get<T>();

        // Get a singleton instance of a type
        public object GetSingleton(Type type) => Kernel.Get(type);

        // Get a singleton instance of a type
        public T GetSingleton<T>() where T : class => Kernel.Get<T>();

        // IoC construct an instance of a type
        public object IoCConstruct(Type type) => Kernel.Get(type);

        // IoC construct an instance of a type
        public T IoCConstruct<T>() where T : class => (T)IoCConstruct(typeof(T));

        // Register a singleton with a constructor function
        public void RegisterSingleton(Type tInterface, Func<object> theConstructor) => Kernel.Bind(tInterface).ToMethod(context => theConstructor()).InSingletonScope();

        // Register a singleton with an object
        public void RegisterSingleton(Type tInterface, object theObject) => Kernel.Bind(tInterface).ToConstant(theObject).InSingletonScope();

        // Register a singleton with a constructor function
        public void RegisterSingleton<TInterface>(Func<TInterface> theConstructor) where TInterface : class => Kernel.Bind<TInterface>().ToMethod(context => theConstructor()).InSingletonScope();

        // Register a singleton with an object
        public void RegisterSingleton<TInterface>(TInterface theObject) where TInterface : class => Kernel.Bind<TInterface>().ToConstant(theObject).InSingletonScope();

        // Register a type mapping
        public void RegisterType(Type tFrom, Type tTo) => Kernel.Bind(tFrom).To(tTo);

        // Register a type with a constructor function
        public void RegisterType(Type t, Func<object> constructor) => Kernel.Bind(t).ToMethod(context => constructor());

        // Register a type with a constructor function
        public void RegisterType<TInterface>(Func<TInterface> constructor) where TInterface : class => Kernel.Bind<TInterface>().ToMethod(context => constructor());

        // Resolve an instance of a type
        public object Resolve(Type type) => Kernel.Get(type);

        // Resolve an instance of a type
        public T Resolve<T>() where T : class => Kernel.Get<T>();

        // Try to resolve an instance of a type
        public bool TryResolve(Type type, out object resolved)
        {
            resolved = Kernel.TryGet(type);
            return (resolved != null);
        }

        // Try to resolve an instance of a type
        public bool TryResolve<T>(out T resolved) where T : class
        {
            resolved = Kernel.TryGet<T>();
            return (resolved != null);
        }

        // Register a type mapping
        void IMvxIoCProvider.RegisterType<TFrom, TTo>() => Kernel.Bind<TFrom>().To<TTo>();

        // Execute any delayed callbacks
        public void ExecuteDelayedCallback()
        {
            while (_delayedCallbacks.Any())
            {
                var dequeuedAction = _delayedCallbacks.Dequeue();

                dequeuedAction?.Action?.Invoke();
            }
        }

        // Not implemented methods for IoC construction with arguments

        public T IoCConstruct<T>(IDictionary<string, object> arguments) where T : class
        {
            throw new NotImplementedException();
        }

        public T IoCConstruct<T>(object arguments) where T : class
        {
            throw new NotImplementedException();
        }

        public T IoCConstruct<T>(params object[] arguments) where T : class
        {
            throw new NotImplementedException();
        }

        public object IoCConstruct(Type type, IDictionary<string, object> arguments)
        {
            throw new NotImplementedException();
        }

        public object IoCConstruct(Type type, object arguments)
        {
            throw new NotImplementedException();
        }

        public object IoCConstruct(Type type, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        // Not implemented method for creating a child container
        public IMvxIoCProvider CreateChildContainer()
        {
            throw new NotImplementedException();
        }
    }
}
