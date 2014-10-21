using System.Windows;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;


namespace BookLibrary
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            //#region Setup Cinch with PRISM/CAL supporting service

            ////********************************************************************
            ////METHOD 1 : 
            ////
            ////Add ViewManager to Cinch directly, to allow it to use 
            ////PRISM/CAL regions. This is prefferred method
            ////********************************************************************
            //IRegionManager regionManager = base.Container.Resolve<IRegionManager>();
            //Cinch.ViewModelBase.ServiceProvider.Add(typeof(IViewManager),
            //    new ViewManager(regionManager));




            ////********************************************************************
            ////METHOD 2 : 
            ////
            ////Add ViewManager to Cinch via Unity, to allow it to use 
            ////PRISM/CAL regions. This is strictly not really required, as all it does
            ////is make the Unity IOC container aware of the IViewManager service. However
            ////any interaction with the IViewManager and Cinch will be via the 
            ////Cinch ViewModelBase.ServiceProvider and Cinch ViewModelBase.Resolve<T>() 
            ////methods.
            ////
            ////I just thought it good practice to add it to Unity, on the off chance
            ////that it may be used elsewhere. Though as I say with Cinch this is very 
            ////unlikely
            ////********************************************************************

            ////base.Container.RegisterType<IViewManager,ViewManager>(
            ////     new InjectionConstructor(base.Container.Resolve<IRegionManager>())); 

            ////var viewManager = base.Container.Resolve<IViewManager>();
            ////Cinch.ViewModelBase.ServiceProvider.Add(typeof(IViewManager), viewManager);

            //#endregion

            //Create CAL shell
            //NOTE : Shell is without modules, as I want it to be a 
            //bulk standard MVVM app just with added
            //regionManager support)
            Shell shell = new Shell();
            shell.Show();
            return shell;
        }

        protected override IModuleCatalog GetModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();
            return catalog;
        }
    }
}
