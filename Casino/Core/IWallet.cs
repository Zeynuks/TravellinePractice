namespace Casino.Core
{
    public interface IWallet
    {
        public Money Balance { get; }
        public void Debit( Money amount );
        public void Credit( Money amount );
    }
}