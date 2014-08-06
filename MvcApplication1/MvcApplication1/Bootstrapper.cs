using System.Web.Mvc;
using Data.Birthday.RepositoryInterfaces;
using Microsoft.Practices.Unity;
using MvcApplication1.Controllers;
using Unity.Mvc4;
using Microsoft.Practices.Unity;

namespace MvcApplication1
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
       // container.RegisterType<BirthdayInterface, BirthdayController>("Birthday");
    }
  }
}