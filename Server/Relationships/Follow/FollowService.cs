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
            if (!(_blockRepository.getBlocksBySender(sender).Any(b => b.getReceiver() == receiver) || _blockRepository.getBlocksOfReceiver(receiver).Any(b => b.getSender() == sender)))
            {
                Follow followToBeAdded = new Follow(sender, receiver);
                _followRepository.addFollow(followToBeAdded);
            }
        }
        public void removeFollow(string sender, string receiver)
        {
            _followRepository.removeFollow(sender, receiver);
        }

        public void updateCloseFriendStatus(string sender, string receiver)
        {
            if (_followRepository.getFollowsFromDictionary().ContainsKey(sender))
            {
                List<Follow> SenderFollows = _followRepository.getFollowersOf(sender);
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
            return _followRepository.getFollowersOf(sender);
        }

        public List<String> getCloseFriendsOf(string sender)
        {
            foreach (Follow follow in _followRepository.getFollowersOf(sender))
            {
                if (follow.getCloseFriendStatus() == true)
                {
                    return follow.getReceiver();
                }
            }
            //return _followRepository.getFollowersOf(sender)
            //                        .filter( f => f.getCloseFriendStatus() == true)
            //                        .getSender();
        }

        public List<Follow> getFollowingOf(string receiver)
        {
            return _followRepository.getFollowingOf(receiver);
        }

        public Dictionary<string, List<Follow>> getAllFollowers()
        {
            return _followRepository.getFollowsFromDictionary();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
