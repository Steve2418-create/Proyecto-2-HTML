using Proyecto_2_HTML;
using Proyecto_2_HTML.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Proyecto_2_HTML
{
    // CAMBIO IMPORTANTE: La clase debe llamarse 'Global' para coincidir con tu archivo
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}