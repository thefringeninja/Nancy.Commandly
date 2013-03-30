namespace Nancy.Commandly.Web.ViewModels
{
    public interface ICommandForm<out TCommand> where TCommand : class
    {
        TCommand Input { get; }
        string Action { get; }
    }
}