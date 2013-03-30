using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nancy.Commandly.Web.Model
{
    public class Handlers
    {
        public void Handle(DeleteAllTheStreams message)
        {
            // pretend he didn't have permissions
            throw new InvalidOperationException("Nice try hacker");
        }

        public void Handle(PingMe message)
        {
            // no op
        }
    }
}