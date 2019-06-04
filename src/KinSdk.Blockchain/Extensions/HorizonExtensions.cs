using KinSdk.Horizon.Models;

namespace KinSdk.Horizon.Extensions
{
    public static class HorizonEnvironmentExtensions
    {
        public static IHorizon Server(this HorizonEnvironment environment)
        {
            return HorizonFactory.Create(environment);
        }
    }
}
