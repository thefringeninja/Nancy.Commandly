Nancy.Commandly
===============
Nancy Commandly is a little demo app I threw together in a couple of hours to show how you can marry Nancy's convention based approach to the C in CQRS with little effort. Cause seriously, fuck effort - especially when you already have your DTOs and time is limited.

Don't like the ugly presentation? No problem! Just use Nancy's view location conventions to supercede the ugly default form I've supplied.

Nancy.Commandly.Tests
===============
Unless you're modelling something for an island full of Cargo Cultists, chances are every business person you work with uses a browser. This test project uses Simple.Testing and a little façade to take advantage of Simple.Testing's use of ToString(). All you need to do is have a quick convo with business users to explain the difference between GET and POST - "GET is when you click a link. POST is when you hit a button and submit to the server." Done.

One other thing... `The Browser's Response Status Code Equals((object)(O K)) Passed` is probably readable enough, but `The Browser's Response Body get  Item("form input[name='name']") Single() Attributes get  Item("value") Equals(" Ninja  Pro") Passed` clearly isn't. Fortunately Simple.Testing's magic comes in its use of expression trees. You can easily extract a little extension method based DSL and make it read like `The Browser's Response Displays(" Ninja Pro") In The Field ("name")`.

Cool story bro, where can I get it?
===============
CTL-A CTL-C ALT-TAB CTL-V

TODO
===============
- Convention based error handling. An InvalidOperationException inside your domain is usually a result of the user putting in something wrong - I would go with 400 Bad Request there. Did your event store throw a ConcurrencyException? 409 Conflct. But of course that should be up to the team and totally configurable.
- A helper with a dsl to generate links to command-forms fluently.
- a way to configure the response - if I've deactivated an inventory item, then I should not even see a link to the form that lets me perform that action again. Or perhaps my user is not authorized to peform that action at this time. etc.
- An actual read model
