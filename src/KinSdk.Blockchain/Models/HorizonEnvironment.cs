using System;
using System.Collections.Generic;
using System.Text;

namespace KinSdk.Horizon.Models
{
    public class HorizonEnvironment
    {
        public string Name { get; }
        public string Hostname { get; }
        public string Passphrase { get; }

        internal HorizonEnvironment(string name, string hostname, string passphrase)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Hostname = hostname ?? throw new ArgumentNullException(nameof(hostname));
            Passphrase = passphrase ?? throw new ArgumentNullException(nameof(passphrase));
        }
    }
}
