using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nancy.Commandly.Web.Modules
{
    public class DefaultModule : NancyModule
    {
        public DefaultModule()
        {
            Get["/"] = _ => Negotiate.WithView("home");
        }
    }
}