using UBB_SE_2024_Gaborment.Server.Relationships.Block;
using UBB_SE_2024_Gaborment.Server.Mocks;

namespace UBB_SE_2024_Gaborment.Server.Relationships.Follow
{
    internal class FollowService
    {
        
        private BlockRepository _blockRepository;
        private FollowRepository _followRepository;
        private UserServiceMock _userServiceMock;

        public FollowService(BlockRepository blockRepository, FollowRepository followRepository)
        {
            _blockRepository = blockRepository;
            _followRepository = followRepository;
            _userServiceMock = new UserServiceMock();
        }

        public FollowService(BlockRepository blockRepository, FollowRepository followRepository, UserServiceMock userServiceMock)
        {
            _blockRepository = blockRepository;
            _followRepository = followRepository;
            _userServiceMock = userServiceMock;
        }

        FollowRepository getFollowRepository()
        {
            return _followRepository;
        }

        BlockRepository getBlockRepository()
        {
            return _blockRepository;
        }
        

        public void createFollow(string sender, string receiver)
        {
            if (!(_blockRepository.GetBlocksBySender(sender).Any(b => b.getReceiver() == receiver) || _blockRepository.GetBlocksOfReceiver(receiver).Any(b => b.getSender() == sender) || _followRepository.GetFollowersOf(sender).Any(f => f.getReceiver() == receiver)))
            {
                Follow followToBeAdded = new Follow(sender, receiver);
                _followRepository.AddFollow(followToBeAdded);
            }
        }
        public void removeFollow(string sender, string receiver)
        {
            
            _followRepository.RemoveFollow(sender, receiver);
            
        }

        public void updateCloseFriendStatus(string sender, string receiver)
        {
            if (this.getAllFollowers().ContainsKey(sender))
            {
                List<Follow> SenderFollows = _followRepository.GetFollowersOf(sender);
                if (SenderFollows.Any(f => f.getReceiver() == receiver) == true)
                {
                    Follow follow = SenderFollows.FirstOrDefault(f => f.getReceiver() == receiver);

                    if (follow != null)
                    {
                        follow.toggleCloseFriend();
                    }
                }
            }
        }

        public List<Follow> getFollowersOf(string sender)
        {
            DateTime now = DateTime.Now;
            return _followRepository.GetFollowersOf(sender).Where(follow => follow.getExpirationTimeStamp() >= now)
                .ToList();
        }

        public List<string> getCloseFriendsOf(string sender)
        {
            DateTime now = DateTime.Now;
            return _followRepository.GetFollowersOf(sender)
                .Where(follow => follow.getCloseFriendStatus() && follow.getExpirationTimeStamp() >= now)
                .Select(follow => follow.getReceiver())
                .ToList();
        }

        public List<Follow> getFollowingOf(string receiver)
        {
            DateTime now = DateTime.Now;
            return _followRepository.GetFollowingOf(receiver)
                .Where(follow => follow.getExpirationTimeStamp() >= now)
                .ToList();
        }

        public List<string> getFollowingUserIdsOf(string receiver)
        {
            DateTime now = DateTime.Now;
            return _followRepository.GetFollowingOf(receiver)
                .Where(follow => follow.getExpirationTimeStamp() >= now)
                .Select(follower => follower.getSender())
                .ToList();
        }

        public Dictionary<string, List<Follow>> getAllFollowers()
        {
            DateTime now = DateTime.Now;
            return _followRepository.GetFollowers()
                .Where(follow => follow.getExpirationTimeStamp() >= now)
                .GroupBy(follow => follow.getSender())
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<UserMock> getFollowersOfAsUserList(string sender)
        {
            DateTime now = DateTime.Now;
            List<string> followList = _followRepository.GetFollowersOf(sender)
                .Where(follow => follow.getExpirationTimeStamp() >= now)
                .Select(follow => follow.getReceiver())
                .ToList();
            List<UserMock> followListUser = new List<UserMock>();
            foreach (string user in followList)
            {
                UserMock FollowedUser = _userServiceMock.GetUserById(user);
                followListUser.Add(FollowedUser);
            }
            return followListUser;
        }

        public List<UserMock> getCloseFriendsOfAsUserList(string sender)
        {
            DateTime now = DateTime.Now;
            List<string> ClosefollowList = _followRepository.GetFollowersOf(sender)
                .Where(follow => follow.getCloseFriendStatus() && follow.getExpirationTimeStamp() >= now)
                .Select(follow => follow.getReceiver())
                .ToList();
            List<UserMock> followListUser = new List<UserMock>();
            foreach (string user in ClosefollowList)
            {
                UserMock FollowedUser = _userServiceMock.GetUserById(user);
                followListUser.Add(FollowedUser);
            }
            return followListUser;
        }

        public List<UserMock> getFollowingOfAsUserList(string receiver)
        {
            DateTime now = DateTime.Now;
            List<string> followList = _followRepository.GetFollowingOf(receiver)
                .Where(follow => follow.getExpirationTimeStamp() >= now)
                .Select(follow => follow.getSender())
                .ToList();
            List<UserMock> followListUser = new List<UserMock>();
            foreach (string user in followList)
            {
                UserMock FollowedUser = _userServiceMock.GetUserById(user);
                followListUser.Add(FollowedUser);
            }
            return followListUser;
        }

    }
}

