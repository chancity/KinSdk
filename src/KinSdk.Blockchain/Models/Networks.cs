using System;
using System.Collections.Generic;
using System.Text;

namespace KinSdk.Horizon.Models
{
    public static class Networks
    {
        public static HorizonEnvironment Testnet = new HorizonEnvironment("Testnet", "https://horizon-testnet.kininfrastructure.com/", "Kin Testnet ; December 2018");
        public static HorizonEnvironment Mainnet = new HorizonEnvironment("Mainnet", "https://horizon.kinfederation.com", "Kin Mainnet ; December 2018");
        public static HorizonEnvironment Custom(string name, string hostname, string passphrase) => new HorizonEnvironment(name, hostname, passphrase);

    }
}
