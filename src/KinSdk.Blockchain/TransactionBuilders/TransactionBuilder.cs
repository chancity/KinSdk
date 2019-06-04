using System.Collections.Generic;
using System.Globalization;
using stellar_dotnet_sdk;

namespace KinSdk.Horizon.TransactionBuilders {
    internal class TransactionBuilder : ITransactionBuilder
    {
        private readonly Transaction.Builder _transactionBuilder;
        private static readonly Asset NativeAsset;
        private readonly List<Operation> _operations;
        private readonly KeyPair _baseKeyPair;
        private readonly KeyPair _channelKeyPair;

        static TransactionBuilder()
        {
            NativeAsset = new AssetTypeNative();
        }

        internal TransactionBuilder(Account sourceAccount, KeyPair baseKeyPair, KeyPair channelKeyPair = null)
        {
            _transactionBuilder = new Transaction.Builder(sourceAccount);
            _baseKeyPair = baseKeyPair;
            _channelKeyPair = channelKeyPair;
            _operations = new List<Operation>();
        }

        public ITransactionBuilder SetMemo(string memo)
        {
            _transactionBuilder.AddMemo(new MemoText(memo));
            return this;
        }

        public ITransactionBuilder SetFee(int fee)
        {
            _transactionBuilder.SetFee(fee);
            return this;
        }

        public ITransactionBuilder AddCreateAccount(string destinationAddress, double startingBalance)
        {
            KeyPair keyPair = KeyPair.FromAccountId(destinationAddress);
            CreateAccountOperation.Builder builder = new CreateAccountOperation.Builder(keyPair, startingBalance.ToString(CultureInfo.InvariantCulture));
            builder.SetSourceAccount(_baseKeyPair);
            _operations.Add(builder.Build());
            return this;
        }

        public ITransactionBuilder AddPayment(string destinationAddress, double amount)
        {
            KeyPair keyPair = KeyPair.FromAccountId(destinationAddress);
            PaymentOperation.Builder builder = new PaymentOperation.Builder(keyPair, NativeAsset, amount.ToString(CultureInfo.InvariantCulture));
            builder.SetSourceAccount(_baseKeyPair);
            return this;
        }
        
        public Transaction Build()
        {
            foreach (Operation operation in _operations)
            {
                _transactionBuilder.AddOperation(operation);
            }
            Transaction transaction = _transactionBuilder.Build();


            transaction.Sign(_baseKeyPair);

            if (_channelKeyPair != null)
            {
                transaction.Sign(_channelKeyPair);
            }
            
            return transaction;
        }
    }
}