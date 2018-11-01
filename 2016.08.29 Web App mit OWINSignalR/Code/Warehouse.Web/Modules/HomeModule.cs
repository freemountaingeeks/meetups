using Nancy;

namespace Warehouse.Web.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/index"] = _ => View["Index.html", this];
            Get["/"] = _ => View["Index.html", this];

            Title = "Warehouse";
        }

        public string Title
        {
            get;
            private set;
        }

    }
}