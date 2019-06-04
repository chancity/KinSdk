using stellar_dotnet_sdk;

namespace KinSdk.Horizon.TransactionBuilders
{
    public interface ITransactionBuilder
    {
        ITransactionBuilder SetMemo(string memo);
        ITransactionBuilder SetFee(int fee);
        ITransactionBuilder AddCreateAccount(string destinationAddress, double startingBalance);
        ITransactionBuilder AddPayment(string destinationAddress, double amount);
        Transaction Build();
    }
}
