# NinjectInMvvmCrossNet
NinjectInMvvmCrossNet 1.0.1 (BETA) - Ninject in MvvmCross

This library allow tu use Ninject with MvvmCross 

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

## Documentation

> The repository is under construction. There will be a dedicated website and proper documentation at some point!

While there is not yet a dedicated documentation

## Contact

logicedinfo@gmail.com

## Donation

If this project help you reduce time to develop, you can give me a cup of coffee :)
https://ko-fi.com/cedricstalder