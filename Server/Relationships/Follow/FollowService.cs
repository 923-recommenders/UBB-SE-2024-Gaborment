using UBB_SE_2024_Gaborment.Server.Relationships.Block;

namespace UBB_SE_2024_Gaborment.Server.Relationships.Follow
{
    internal class FollowService
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private BlockRepository _blockRepository;
        private FollowRepository _followRepository;

        public FollowService(BlockRepository blockRepository, FollowRepository followRepository)
        {
            _blockRepository = blockRepository;
            _followRepository = followRepository;
        }

        FollowRepository getFollowRepository()
        {
            return _followRepository;
        }

        BlockRepository getBlockRepository()
        {
            return _blockRepository;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void createFollow(string sender, string receiver)
        {
            if (!(_blockRepository.GetBlocksBySender(sender).Any(b => b.getReceiver() == receiver) || _blockRepository.GetBlocksOfReceiver(receiver).Any(b => b.getSender() == sender)))
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
            return _followRepository.GetFollowersOf(sender);
        }

        public List<string> getCloseFriendsOf(string sender)
        {
            return _followRepository.GetFollowersOf(sender)
                            .Where(follow => follow.getCloseFriendStatus() == true)
                            .Select(follow => follow.getReceiver())
                            .ToList();  
        }

        public List<Follow> getFollowingOf(string receiver)
        {
            return _followRepository.GetFollowingOf(receiver);
        }

        public List<string> getFollowingUserIdsOf(string receiver)
        {
            return _followRepository.GetFollowingOf(receiver).Select(follower => follower.getSender()).ToList();
        }

        public Dictionary<string, List<Follow>> getAllFollowers()
        {
            return _followRepository.GetFollowers()
                .GroupBy(follow => follow.getSender())
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
