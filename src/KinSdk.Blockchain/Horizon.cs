using System;
using System.Threading.Tasks;
using KinSdk.Horizon.Models;
using KinSdk.Horizon.TransactionBuilders;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;

namespace KinSdk.Horizon
{
    internal class Horizon : IHorizon
    {
        public HorizonEnvironment Environment { get; }
        public Server Server { get; }
        public PaymentsRequestBuilder Payments => Server.Payments;
        public OperationsRequestBuilder Operations => Server.Operations;

        public async Task<ITransactionBuilder> TransactionBuilder(KeyPair baseKeyPair, KeyPair channelKeyPair)
        {
            AccountResponse accountResponse = await GetAccount(channelKeyPair?.AccountId ?? baseKeyPair.AccountId).ConfigureAwait(true);
            Account sourceAccount = new Account(accountResponse.AccountId, accountResponse.SequenceNumber);
            return new TransactionBuilder(sourceAccount, baseKeyPair, channelKeyPair);
        }

        public Horizon(HorizonEnvironment environment)
        {
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            Server = new Server(environment.Hostname);
            Network.Use(new Network(environment.Passphrase));

        }
        
        public Task<AccountResponse> GetAccount(string accountId)
        {
            return Server.Accounts.Account(accountId);
        }

        public Task<SubmitTransactionResponse> SubmitTransaction(Transaction transaction)
        {
            return Server.SubmitTransaction(transaction);
        }
    }
}
