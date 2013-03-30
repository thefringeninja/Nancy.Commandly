using Nancy.Bootstrapper;
using Nancy.Commandly.Web.Model;
using Nancy.Hosting.Aspnet;
using Nancy.TinyIoc;

namespace Nancy.Commandly.Web
{
    public class Bootstrapper : DefaultNancyAspNetBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            RegisterCommands(container);

        }

        private static void RegisterCommands(TinyIoCContainer container)
        {
            var bus = container.Resolve<IBus>();
            var handlers = new Handlers();

            bus.Register<DeleteAllTheStreams>(handlers.Handle);
            bus.Register<PingMe>(handlers.Handle);
        }
    }
}