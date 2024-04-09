using UBB_SE_2024_Gaborment.Server.Mocks;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;

namespace UBB_SE_2024_Gaborment.Server.Relationships.Block
{
    internal class BlockService
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private BlockRepository _blockRepository;
        private FollowRepository _followRepository;
        private UserServiceMock _userServiceMock;

        public BlockService(BlockRepository blockRepository, FollowRepository followRepository)
        {
            _blockRepository = blockRepository;
            _followRepository = followRepository;
            _userServiceMock = new UserServiceMock();
        }
        public BlockService(BlockRepository blockRepository, FollowRepository followRepository, UserServiceMock userService)
        {
            _blockRepository = blockRepository;
            _followRepository = followRepository;
            _userServiceMock = userService;
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
        ///
        public void createBlock(string sender, string receiver, string reason)
        {
            if (_followRepository.GetFollowersOf(sender).Any(f => f.getReceiver() == receiver) == true || _followRepository.GetFollowingOf(receiver).Any(f => f.getSender() == sender) == true || (!_blockRepository.GetBlocksBySender(sender).Any(b => b.getReceiver() == receiver)))
            {
                _followRepository.RemoveFollow(sender, receiver);
            }
            Block blockToBeAdded = new Block(sender, receiver, DateTime.Now, reason);
            _blockRepository.AddBlock(blockToBeAdded);
        }

        public void RemoveBlock(string sender, string receiver)
        {

                _blockRepository.RemoveBlock(sender, receiver);
        }
        
        public List<Block> getBlocksBy(string sender)
        {
            return _blockRepository.GetBlocksBySender(sender);
        }
       
        public List<Block> getBlocksOf(string receiver)
        {
            return _blockRepository.GetBlocksOfReceiver(receiver);
        }

        public List<string> getBlockedUserIdsOf(string receiver)
        {
            return _blockRepository.GetBlocksOfReceiver(receiver).Select(block => block.getSender()).ToList();
        }

        
        public List<Block> getAllBlocks()
        {
            return _blockRepository.GetBlocks();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        public List<UserMock> getBlocksOfAsUserList(string receiver)
        {
            List<string> blockListString = _blockRepository.GetBlocksOfReceiver(receiver).Select(block => block.getSender()).ToList();
            List<UserMock> followListUser = new List<UserMock>();
            foreach (string user in blockListString)
            {
                UserMock FollowedUser = _userServiceMock.GetUserById(user);
                followListUser.Add(FollowedUser);
            }
            return followListUser;
        }

        public List<UserMock> getBlocksByAsUserList(string sender)
        {
            List<string> blockListString = _blockRepository.GetBlocksBySender(sender).Select(block => block.getReceiver()).ToList();
            List<UserMock> followListUser = new List<UserMock>();
            foreach (string user in blockListString)
            {
                UserMock FollowedUser = _userServiceMock.GetUserById(user);
                followListUser.Add(FollowedUser);
            }
            return followListUser;
        }
    }
}
