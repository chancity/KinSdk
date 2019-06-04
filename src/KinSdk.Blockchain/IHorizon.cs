using System.Threading.Tasks;
using KinSdk.Horizon.Models;
using KinSdk.Horizon.TransactionBuilders;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;

namespace KinSdk.Horizon
{
    public interface IHorizon
    {
        HorizonEnvironment Environment { get; }
        Task<AccountResponse> GetAccount(string accountId);
        Task<SubmitTransactionResponse> SubmitTransaction(Transaction transaction);
        PaymentsRequestBuilder Payments { get; }
        OperationsRequestBuilder Operations { get; }
        Task<ITransactionBuilder> TransactionBuilder(KeyPair baseKeyPair, KeyPair channelKeyPair = null);
    }
}
