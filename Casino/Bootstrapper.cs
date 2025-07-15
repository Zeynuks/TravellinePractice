using Casino.UI;
using Casino.Core;

namespace Casino
{
    public static class Bootstrapper
    {
        public static Wallet InitializeWallet( IUserInterface ui )
        {
            ui.ShowBanner();
            while ( true )
            {
                try
                {
                    int initialInput = ui.ReadInt( "Введите начальный баланс:" );
                    Money initialMoney = new( initialInput );
                    
                    return new Wallet( initialMoney );
                }
                catch ( Exception ex )
                {
                    ui.WriteLine( ex.Message );
                }
            }
        }
    }
}