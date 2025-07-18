using Casino.UI;
using Casino.UI.Menu;

namespace Casino
{
    public class App
    {
        private readonly IUserInterface _ui;
        private readonly Menu _menu;

        public App( IUserInterface ui, Menu menu )
        {
            _ui = ui;
            _menu = menu;
        }

        public void Run()
        {
            _menu.Execute();
            _ui.WriteLine( "Удачи в следующий раз!" );
        }
    }
}