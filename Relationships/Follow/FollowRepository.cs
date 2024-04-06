using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Relationships.Follow
{
    internal class FollowRepository
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ///TODOS - CONSTRUCTOR FRO ALREADY FORMED DICTIONARY (maybe checks?)

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        public void addFollow(Follow follow)
        {
            // Check if a list exists for the sender and the receiver. 
            // If it does not exist for any of them, create it
            if (!followsFromDictionary.ContainsKey(follow.getSender()))
            {
                followsFromDictionary[follow.getSender()] = new List<Follow>();
            }
            if (!followsToDictionary.ContainsKey(follow.getReceiver()))
            {
                followsToDictionary[follow.getReceiver()] = new List<Follow>();
            }

            // Check if the follow already exists for the sender
            List<Follow> senderFollows = followsFromDictionary[follow.getSender()];
            bool followExists = senderFollows.Any(f => f.getReceiver() == follow.getReceiver());
            if (!followExists)
            {
                // Add it in the followsFromDictionary at the sender key
                followsFromDictionary[follow.getSender()].Add(follow);
                // Add it in the followsToDictionary at the receiver key
                followsToDictionary[follow.getReceiver()].Add(follow);
            }
        }

        public void removeFollow(string sender, string receiver)
        {
            // Check if the sender exists in followsFromDictionary
            if (followsFromDictionary.ContainsKey(sender) && followsToDictionary.ContainsKey(receiver))
            {
                List<Follow> senderFollows = followsFromDictionary[sender];

                // Check if there are follows from this sender to the receiver
                bool followExists = senderFollows.Any(f => f.getReceiver() == receiver);

                if (followExists)
                {
                    // Remove the Follow from the sender key from followsFromDictionary
                    followsFromDictionary[sender].RemoveAll(f => f.getReceiver() == receiver);

                    // Remove the follow from the receiver key from followsToDictionary 
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
                return new List<Follow>(); // Return an empty list if no following found
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
                return new List<Follow>(); // Return an empty list if no followers found
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
                return null; // Return null if the sender doesn't exist or no follow relationship found
            }
        }
    }
    
}
