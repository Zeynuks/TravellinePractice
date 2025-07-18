namespace Casino.UI.Menu
{
    public interface IMenuComponent
    {
        public string Title { get; }
        void Execute();
    }
}