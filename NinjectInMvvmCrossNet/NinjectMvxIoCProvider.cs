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

        class ActionContainer
        {
            public Type? Type { get; set; }
            public Action? Action { get; set; }
        }
        public NinjectMvxIoCProvider(params INinjectModule[] modules)
        {
            Kernel = new StandardKernel(modules);
        }

        public NinjectMvxIoCProvider(INinjectSettings settings, params INinjectModule[] modules)
        {
            Kernel = new StandardKernel(settings, modules);
        }

        public void CallbackWhenRegistered(Type type, Action action)
        {
            _delayedCallbacks.Enqueue(new ActionContainer() { Action = action, Type = type });
        }

        public void CallbackWhenRegistered<T>(Action action)
        {
            CallbackWhenRegistered(typeof(T), action);
        }

        public bool CanResolve(Type type)
        {
            return (bool)Kernel.CanResolve(type);
        }

        public bool CanResolve<T>() where T : class => Kernel.CanResolve<T>();

        public object Create(Type type) => Kernel.Get(type);

        public T Create<T>() where T : class => Kernel.Get<T>();

        public object GetSingleton(Type type) => Kernel.Get(type);

        public T GetSingleton<T>() where T : class => Kernel.Get<T>();

        public object IoCConstruct(Type type) => Kernel.Get(type);

        public T IoCConstruct<T>() where T : class => (T)IoCConstruct(typeof(T));

        public void RegisterSingleton(Type tInterface, Func<object> theConstructor) => Kernel.Bind(tInterface).ToMethod(context => theConstructor()).InSingletonScope();

        public void RegisterSingleton(Type tInterface, object theObject) => Kernel.Bind(tInterface).ToConstant(theObject).InSingletonScope();

        public void RegisterSingleton<TInterface>(Func<TInterface> theConstructor) where TInterface : class => Kernel.Bind<TInterface>().ToMethod(context => theConstructor()).InSingletonScope();

        public void RegisterSingleton<TInterface>(TInterface theObject) where TInterface : class => Kernel.Bind<TInterface>().ToConstant(theObject).InSingletonScope();

        public void RegisterType(Type tFrom, Type tTo) => Kernel.Bind(tFrom).To(tTo);

        public void RegisterType(Type t, Func<object> constructor) => Kernel.Bind(t).ToMethod(context => constructor());

        public void RegisterType<TInterface>(Func<TInterface> constructor) where TInterface : class => Kernel.Bind<TInterface>().ToMethod(context => constructor());

        public object Resolve(Type type) => Kernel.Get(type);

        public T Resolve<T>() where T : class => Kernel.Get<T>();


        public bool TryResolve(Type type, out object resolved)
        {
            resolved = Kernel.TryGet(type);
            return (resolved != null);
        }

        public bool TryResolve<T>(out T resolved) where T : class
        {
            resolved = Kernel.TryGet<T>();
            return (resolved != null);
        }

        void IMvxIoCProvider.RegisterType<TFrom, TTo>() => Kernel.Bind<TFrom>().To<TTo>();

        public void ExecuteDelayedCallback()
        {
            while (_delayedCallbacks.Any())
            {
                var dequeuedAction = _delayedCallbacks.Dequeue();

                dequeuedAction?.Action?.Invoke();
            }
        }
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

        public IMvxIoCProvider CreateChildContainer()
        {
            throw new NotImplementedException();
        }
    }
}
