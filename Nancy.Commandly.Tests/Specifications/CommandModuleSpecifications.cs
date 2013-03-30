using System;
using System.Linq;
using Nancy.Commandly.Tests.Fixtures;
using Nancy.Commandly.Tests.SpecificationTemplates;
using Nancy.Testing;
using Simple.Testing.ClientFramework;

namespace Nancy.Commandly.Tests.Specifications
{
    public class CommandModuleSpecifications
    {
        private static readonly Guid aggregateId = Guid.NewGuid();
        public Specification when_collecting_user_input_for_a_command()
        {
            return new NancyResponseSpecification<TestCommandModule>
            {
                OnContext = context =>
                {
                    context.Header("Accept", "text/html");
                    context.Query("name", "Ninja Pro");
                    context.Query("description", "Allows you to move silently.");
                },
                When = context => UserAgent.Get("/dispatcher/test", context),
                Expect =
                {
                    result => result.Response.StatusCode.Equals(HttpStatusCode.OK),
                    result => result.Response.Body.AsString().Contains("Razor Compilation Error").Equals(false),
                    result => result.Response.Body["form"].Count().Equals(1),
                    result => result.Response.Body["form"].Single().Attributes["method"] == "POST",
                    result => result.Response.Body["form"].Single().Attributes["action"] == "/dispatcher/test",
                    result => result.Response.Body["form input[name='name']"].Count().Equals(1),
                    result => result.Response.Body["form input[name='name']"].Single().Attributes["type"].Equals("text"),
                    result => result.Response.Body["form input[name='name']"].Single().Attributes["value"].Equals("Ninja Pro"),
                    result => result.Response.Body["form input[name='date']"].Single().Attributes["type"].Equals("datetime"),
                    result => result.Response.Body["form input[name='isChecked']"].Single().Attributes["type"].Equals("checkbox"),

                }
            };
        }

        public Specification when_sending_a_command()
        {
            return new NancyResponseSpecification<TestCommandModule>
            {
                When = context => UserAgent.Post("/dispatcher/test", context),
                OnContext = context =>
                {
                    context.FormValue("name", "Ninja Pro");
                    context.FormValue("description", "Allows you to move silently.");
                    context.FormValue("aggregateId", aggregateId.ToString());
                    context.FormValue("somethingThatDoesntBelongId", Guid.Empty.ToString());
                },
                Expect =
                {
                    result => result.Response.StatusCode.Equals(HttpStatusCode.OK),
                    result => result.Dispatched.Count().Equals(1),
                    result => result.Dispatched.Single().GetType() == typeof (Test),
                    result => result.Dispatched.OfType<Test>().Single().AggregateId.Equals(aggregateId),
                    result => result.Dispatched.OfType<Test>().Single().Name.Equals("Ninja Pro"),
                    result => result.Dispatched.OfType<Test>().Single().Description.Equals("Allows you to move silently.")
                }
            };
        }
    }
}