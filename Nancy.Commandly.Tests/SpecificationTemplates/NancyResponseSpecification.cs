using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nancy.Commandly.Web;
using Nancy.Commandly.Web.Model;
using Nancy.Testing;
using Simple.Testing.ClientFramework;
using NancyBrowser = Nancy.Testing.Browser;
namespace Nancy.Commandly.Tests.SpecificationTemplates
{
    public class NancyResponseSpecification<TModule> :
        TypedSpecification<NancyResponseSpecification<TModule>.Browser>
        where TModule : INancyModule
    {
        private readonly FakeBus bus;
        public Action Before;
        public Action<ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator> Bootstrap = with => { };
        public List<Expression<Func<Browser, bool>>> Expect = new List<Expression<Func<Browser, bool>>>();
        public Action Finally;
        public string Name;


        public Action<BrowserContext> OnContext = context => { };

        public Func<Action<BrowserContext>, UserAgent> When;

        public NancyResponseSpecification()
        {
            bus = new FakeBus();
        }

        #region TypedSpecification<NancyResponseSpecification<TModule>.BrowserResult> Members

        public string GetName()
        {
            return Name;
        }

        public Action GetBefore()
        {
            return Before;
        }

        public Delegate GetOn()
        {
            return new Func<UserAgent>(() => When(OnContext));
        }

        public Delegate GetWhen()
        {
            return new Func<UserAgent, Browser>(
                userAgent => new Browser(
                                 userAgent.Execute(
                                     new NancyBrowser(with => Bootstrap(
                                         with.Module<TModule>().Dependency<IBus>(bus)))), bus));
        }

        public IEnumerable<Expression<Func<Browser, bool>>> GetAssertions()
        {
            return Expect;
        }

        public Action GetFinally()
        {
            return Finally;
        }

        #endregion

   
        #region Nested type: BrowserResult

        public class Browser
        {
            public Browser(BrowserResponse response, FakeBus bus)
            {
                Dispatched = new List<object>(bus.SentCommands).AsReadOnly();
                Response = response;
            }

            public IEnumerable<object> Dispatched
            {
                get; set;
            }

            public BrowserResponse Response { get; private set; }

            public override string ToString()
            {
                var responseBuilder =
                    new StringBuilder().Append("HTTP/1.1 ")
                                       .Append((int) Response.StatusCode)
                                       .Append(' ')
                                       .Append(Response.StatusCode.ToString().Titleize())
                                       .AppendLine();
                responseBuilder = Response.Headers.Aggregate(responseBuilder,
                                                             (builder, header) =>
                                                             builder.Append(header.Key)
                                                                    .Append(": ")
                                                                    .Append(header.Value)
                                                                    .AppendLine());
                return responseBuilder.ToString();
            }
        }

        #endregion

        #region Nested type: FakeBus

        public class FakeBus : IBus
        {
            private readonly List<object> sentCommands = new List<object>();

            public IEnumerable<object> SentCommands
            {
                get { return sentCommands; }
            }

            #region IBus Members

            public void Register<T>(Action<T> handler) where T : class
            {
            }

            public void Publish<T>(T @event) where T : class
            {
            }

            public void Send<T>(T command) where T : class
            {
                sentCommands.Add(command);
            }

            #endregion
        }

        #endregion
    }
}