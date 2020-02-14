using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BookWorm
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            // Handle HTTP errors
            if (exc.GetType() == typeof(HttpException))
            {
                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                    return;
            }
            Response.Write("<h2>Global Page Error</h2>\n");
            Response.Write(
                "<p>" + exc.Message + "</p>\n");
            Response.Write("Return to the <a href='Index.aspx'>" +
                "Default Page</a>\n");
            Response.Redirect("HttpErrorPage.aspx");
            Server.ClearError();
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
