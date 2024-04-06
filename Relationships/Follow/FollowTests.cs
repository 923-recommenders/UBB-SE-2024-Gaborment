﻿using recommenders_backend.Relationships.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend.Relationships.Follow
{
    internal class FollowTests
    {
        public static void TestFollowClass()
        {

            
            Follow follow1 = new Follow("senderId", "receiverId");
            if (follow1.getSender() != "senderId" || follow1.getReceiver() != "receiverId" || follow1.getDescription() != "")
                Console.WriteLine("Follow constructor 1 failed.");
            Follow follow2 = new Follow("senderId", "receiverId", "This is a test description");
            if (follow2.getSender() != "senderId" || follow2.getReceiver() != "receiverId" || follow2.getDescription() != "This is a test description")
                Console.WriteLine("Follow constructor 2 failed.");

            
            follow1.toggleCloseFriend();
            if (!follow1.getCloseFriendStatus())
                Console.WriteLine("toggleCloseFriend failed.");
            follow1.toggleCloseFriend();
            if (follow1.getCloseFriendStatus())
                Console.WriteLine("toggleCloseFriend failed.");

            
            DateTime newTimeStamp = DateTime.Now.AddDays(7);
            follow1.setExpirationTimeStamp(newTimeStamp);
            if (follow1.getExpirationTimeStamp() != newTimeStamp)
                Console.WriteLine("setExpirationTimeStamp failed.");
        }

        public static void TestFollowRepositoryClass()
        {
            FollowRepository repository = new FollowRepository();

            
            Follow follow1 = new Follow("senderId", "receiverId");
            repository.addFollow(follow1);
            if (repository.getFollow("senderId", "receiverId") == null)
                Console.WriteLine("addFollow or getFollow failed.");

            
            repository.removeFollow("senderId", "receiverId");
            if (repository.getFollowersOf("receiverId").Count != 0 || repository.getFollowingOf("senderId").Count != 0 || repository.getFollow("senderId", "receiverId") != null)
                Console.WriteLine("removeFollow failed.");

            
            Follow follow2 = new Follow("senderId2", "receiverId");
            repository.addFollow(follow2);
            List<Follow> followers = repository.getFollowingOf("receiverId");
            if (followers.Count != 1 || followers[0].getSender() != "senderId2")
                Console.WriteLine("getFollowersOf failed.");
            List<Follow> following = repository.getFollowersOf("senderId2");
            if (following.Count != 1 || following[0].getReceiver() != "receiverId")
                Console.WriteLine("getFollowingOf failed.");
        }

        public static void TestFollowServiceClass()
        {
           
            FollowRepository followRepository = new FollowRepository();
            BlockRepository blockRepository = new BlockRepository();
            FollowService followService = new FollowService(blockRepository, followRepository);

            
            followService.createFollow("sender1", "receiver1");
            if (followRepository.getFollowingOf("receiver1").Count != 1 || followRepository.getFollowersOf("sender1").Count != 1)
                Console.WriteLine("createFollow failed.");

           
            followService.removeFollow("sender1", "receiver1");
            if (followRepository.getFollowingOf("receiver1").Count != 0 || followRepository.getFollowersOf("sender1").Count != 0)
                Console.WriteLine("removeFollow failed.");

           
            followService.createFollow("sender1", "receiver1");
            followService.updateCloseFriendStatus("sender1", "receiver1");
            Follow follow = followRepository.getFollowingOf("receiver1")[0];
            if (!follow.getCloseFriendStatus())
                Console.WriteLine("updateCloseFriendStatus failed.");

            
            List<Follow> followers = followService.getFollowingOf("receiver1");
            if (followers.Count != 1 || followers[0].getSender() != "sender1")
                Console.WriteLine("getFollowersOf failed.");

            
            List<Follow> following = followService.getFollowersOf("sender1");
            if (following.Count != 1 || following[0].getReceiver() != "receiver1")
                Console.WriteLine("getFollowingOf failed.");

            
            Dictionary<string, List<Follow>> allFollowers = followService.getAllFollowers();
            if (!allFollowers.ContainsKey("sender1") || allFollowers["sender1"].Count != 1 || allFollowers["sender1"][0].getReceiver() != "receiver1")
                Console.WriteLine("getAllFollowers failed.");
        }
    }
}
