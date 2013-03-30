namespace Nancy.Commandly.Web.ViewModels
{
    public class CommandFormViewModel<TCommand> : ICommandForm<TCommand> where TCommand: class
    {
        public TCommand Input { get; protected set; }
        public string Action { get; private set; }
        public CommandFormViewModel(TCommand input, string modulePath)
        {
            Input = input;
            Action = modulePath;
        }
    }
}