using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend.Relationships.Follow
{
    internal class FollowRepository
    {
        
        private Dictionary<string, List<Follow>> followsFromDictionary;
        private Dictionary<string, List<Follow>> followsToDictionary;

        public FollowRepository()
        {
            followsFromDictionary = new Dictionary<string, List<Follow>>();
            followsToDictionary = new Dictionary<string, List<Follow>>();
        }

        public Dictionary<string, List<Follow>> getFollowsFromDictionary()
        {
            return this.followsFromDictionary;
        }

        public Dictionary<string, List<Follow>> getFollowsToDictionary()
        {
            return this.followsToDictionary;
        }


        public void addFollow(Follow follow)
        {

            if (!followsFromDictionary.ContainsKey(follow.getSender()))
            {
                followsFromDictionary[follow.getSender()] = new List<Follow>();
            }
            if (!followsToDictionary.ContainsKey(follow.getReceiver()))
            {
                followsToDictionary[follow.getReceiver()] = new List<Follow>();
            }

            List<Follow> senderFollows = followsFromDictionary[follow.getSender()];
            bool followExists = senderFollows.Any(f => f.getReceiver() == follow.getReceiver());
            if (!followExists)
            {
                followsFromDictionary[follow.getSender()].Add(follow);
                followsToDictionary[follow.getReceiver()].Add(follow);
            }
        }

        public void removeFollow(string sender, string receiver)
        {
            if (followsFromDictionary.ContainsKey(sender) && followsToDictionary.ContainsKey(receiver))
            {
                List<Follow> senderFollows = followsFromDictionary[sender];

                bool followExists = senderFollows.Any(f => f.getReceiver() == receiver);

                if (followExists)
                {
                    followsFromDictionary[sender].RemoveAll(f => f.getReceiver() == receiver);

                    followsToDictionary[receiver].RemoveAll(f => f.getSender() == sender);
                }
            }
        }

        public List<Follow> getFollowersOf(string sender)
        {
            if (followsFromDictionary.ContainsKey(sender))
            {
                return followsFromDictionary[sender];
            }
            else
            {
                return new List<Follow>(); 
            }
        }

        public List<Follow> getFollowingOf(string receiver)
        {
            if (followsToDictionary.ContainsKey(receiver))
            {
                return followsToDictionary[receiver];
            }
            else
            {
                return new List<Follow>(); 
            }
        }

        public Follow getFollow(string sender, string receiver)
        {
            if (followsFromDictionary.ContainsKey(sender))
            {
                return followsFromDictionary[sender].FirstOrDefault(f => f.getReceiver() == receiver);
            }
            else
            {
                return null; 
            }
        }
    }
    
}
