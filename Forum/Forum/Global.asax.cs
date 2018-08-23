namespace Forum
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Ninject;
    using Infrastructure;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(new StandardKernel()));
        }
    }
}
