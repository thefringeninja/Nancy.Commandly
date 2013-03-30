using Nancy.Commandly.Web.Model;
using Nancy.Commandly.Web.ViewModels;
using Nancy.ModelBinding;

namespace Nancy.Commandly.Web.Modules
{
    public class PingMeModule : CommandModule<PingMe>
    {
        public PingMeModule(IBus bus, INancyContextFactory contextFactory) : base(bus, contextFactory)
        {

        }

        protected override Response OnDispatched()
        {
            return 202;
        }
    }

    public class DeleteAllTheStreamsModule : CommandModule<DeleteAllTheStreams>
    {
        public DeleteAllTheStreamsModule(IBus bus, INancyContextFactory contextFactory) : base(bus, contextFactory)
        {
        }

        protected override Response OnDispatched()
        {
            return Response.AsRedirect("/");
        }
    }

    public class NoHandlerRegisteredModule : CommandModule<NoHandlerRegistered>
    {
        public NoHandlerRegisteredModule(IBus bus, INancyContextFactory contextFactory) : base(bus, contextFactory)
        {
        }
    }

    public abstract class CommandModule<TCommand> : NancyModule where TCommand : class
    {
        protected CommandModule(IBus bus, INancyContextFactory contextFactory)
            : base("/dispatcher/" + typeof (TCommand).Name.Underscore().Dasherize())
        {
            Get["/"] = p =>
            {
                var useCase = GetUseCaseFromQueryString(contextFactory);
                var viewModel = new CommandFormViewModel<TCommand>(useCase, ModulePath);
                return Negotiate.WithModel(viewModel).WithView("get");
            };
            Post["/"] = p =>
            {
                var command = this.Bind<TCommand>();

                bus.Send(command);

                return OnDispatched();
            };
        }

        private TCommand GetUseCaseFromQueryString(INancyContextFactory contextFactory)
        {
            var context = contextFactory.Create(Request);
            
            foreach (var key in context.Request.Query)
            {
                context.Request.Form.Add(key, context.Request.Query[key]);
            }

            var binder = ModelBinderLocator.GetBinderForType(typeof (TCommand), context);
            
            var useCase = (TCommand) binder.Bind(context, typeof (TCommand), null, BindingConfig.Default);
            
            return useCase;
        }

        // override this if handling a certain command has different semantics
        // i.e. if it kicks off a Saga (I mean Process Manager lol) then you should probably return 202 Accepted
        // or maybe you always want to redirect them to the home page etc.
        protected virtual Response OnDispatched()
        {
            return HttpStatusCode.OK;
        }
    }
}