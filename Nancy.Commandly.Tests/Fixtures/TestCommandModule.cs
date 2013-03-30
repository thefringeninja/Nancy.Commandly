using Nancy.Commandly.Web.Model;
using Nancy.Commandly.Web.Modules;

namespace Nancy.Commandly.Tests.Fixtures
{
    public class TestCommandModule : CommandModule<Test>
    {
        public TestCommandModule(IBus bus, INancyContextFactory contextFactory) : base(bus, contextFactory)
        {
        }
    }
}