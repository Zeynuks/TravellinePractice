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
                    Money initialMoney = ui.ReadValue<Money>( "Введите начальный баланс:" );

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