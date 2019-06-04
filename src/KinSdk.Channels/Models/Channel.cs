using System;

namespace KinSdk.ChannelManager.Models
{
    internal class Channel
    {
        public string Seed { get; }
        public ChannelState State { get; private set; }

        public Channel(string seed)
        {
            Seed = seed ?? throw new ArgumentNullException(nameof(seed));
            State = ChannelState.NotFound;
        }

        public void SetState(ChannelState state)
        {
            State = state;
        }

        private bool Equals(Channel other)
        {
            return string.Equals(Seed, other.Seed);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Channel) obj);
        }

        public override int GetHashCode()
        {
            return (Seed != null ? Seed.GetHashCode() : 0);
        }
    }
}
