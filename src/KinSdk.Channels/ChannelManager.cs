using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using KinSdk.ChannelManager.Models;

namespace KinSdk.ChannelManager
{
    internal class ChannelManager : IChannelManager
    {
        private static readonly Random GetRandom;
        private readonly HashSet<Channel> _channels;
        private readonly object _lockObject;

        static ChannelManager()
        {
            GetRandom = new Random();
    }
        public ChannelManager()
        {
            _channels = new HashSet<Channel>();
            _lockObject = new object();
        }

        public bool AddChannel(string seed)
        {
            lock (_lockObject)
            {
                return _channels.Add(new Channel(seed));
            }
        }

        public bool RemoveChannel(string seed)
        {
            lock (_lockObject)
            {
                var channel = _channels.SingleOrDefault(x => x.Seed == seed);
                return _channels.Remove(channel);
            }
        }

        public bool ReleaseChannel(string seed)
        {
            lock (_lockObject)
            {
                var channel = _channels.SingleOrDefault(x => x.Seed == seed);
                channel?.SetState(ChannelState.Free);
                return true;
            }
        }
        public string GetFreeChannelAndLock()
        {
            var channel = _GetFreeChannel();
            channel.SetState(ChannelState.Locked);
            return channel.Seed;
        }

        public string GetFreeChannel()
        {
            var channel = _GetFreeChannel();
            return channel.Seed;
        }
        private Channel _GetFreeChannel()
        {
            List<Channel> availableChannels;
            var totalChannels = 0;

            lock (_lockObject)
            {
                availableChannels = _channels.Where(key => key.State == ChannelState.Free).ToList();
                totalChannels = _channels.Count;
            }

            if (totalChannels == 0)
            {
                throw new Exception("No channels configured");
            }

            if (availableChannels.Count == 0)
            {
                throw new Exception("No available channels");
            }

            return availableChannels[GetRandomNumber(0, availableChannels.Count)];
        }

        private static int GetRandomNumber(int min, int max)
        {
            lock (GetRandom)
            {
                return GetRandom.Next(min, max);
            }
        }
    }
}
