using System.Collections.Concurrent;
using KinSdk.Horizon.Models;

namespace KinSdk.Horizon
{
    internal static class HorizonFactory
    {
        private static readonly ConcurrentDictionary<string, Horizon> Horizons;
        static HorizonFactory()
        {
            Horizons = new ConcurrentDictionary<string, Horizon>();
        }

        public static Horizon Create(HorizonEnvironment environment)
        {
            var key = $"{environment.Name}:{environment.Passphrase}:{environment.Hostname}";
            return Horizons.GetOrAdd(key, new Horizon(environment));
        }

    }
}
