namespace OrderManager.UI.Menu
{
    public interface IMenuComponent
    {
        public string Title { get; }
        void Execute();
    }
}