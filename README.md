# NinjectToMvvmCross
NinjectToMvvmCross 1.0.1 - Ninject in MvvmCross

This is a Ninject IoC provider for MvvmCross. 

## Usage

    // use normal ninject modeul binding 
    internal class projectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICarViewModel>().To<CarViewModel>();
            Bind<IInsuranceCarManager>().To<InsuranceCarManager>().InSingletonScope();
            ......
        }
    }

    // in your Setup class ovveride the CreateIocProvider() method to return a new instance of NinjectMvxIoCProvider with an array of diffrent Modul created
    public class Setup : MvxWpfSetup<AppViewModel>
    {
        protected override IMvxIoCProvider CreateIocProvider() => new NinjectMvxIoCProvider(new projectModule());

    }

## Contact

logicedinfo@gmail.com

## Donation

If this project help you reduce time to develop, you can give me a cup of coffee :)
https://ko-fi.com/cedricstalder