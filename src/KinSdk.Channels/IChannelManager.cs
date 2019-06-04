namespace KinSdk.ChannelManager
{
   public interface IChannelManager
   {
       bool AddChannel(string seed);
       bool RemoveChannel(string seed);
       bool ReleaseChannel(string seed);
       string GetFreeChannel();
    }
}
