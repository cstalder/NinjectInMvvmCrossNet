using MvvmCross.IoC;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.ViewModels;

namespace NinjectInMvvmCrossNet
{
    public abstract class NinjectMvxNetSetup<TApplication> : MvxWpfSetup<TApplication>
        where TApplication : class, IMvxApplication, new()
    {
        protected override void InitializeLastChance(IMvxIoCProvider iocProvider)
        {
            base.InitializeLastChance(iocProvider);

            (NinjectMvxIoCProvider.Instance as NinjectMvxIoCProvider)?.ExecuteDelayedCallback();
        }


    }
}
