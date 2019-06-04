using KinSdk.ChannelManager.Models;

namespace KinSdk.ChannelManager
{
   public interface IChannelManager
   {
       bool AddChannel(string seed);
       bool RemoveChannel(string seed);
       bool SetChannelState(string seed, ChannelState state);
       string GetFreeChannel();
       string GetFreeChannelAndLock();
   }
}
