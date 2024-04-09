using UBB_SE_2024_Gaborment.Server.Relationships.Follow;

namespace UBB_SE_2024_Gaborment.Server.Relationships.Block
{
    internal class BlockService
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private BlockRepository _blockRepository;
        private FollowRepository _followRepository;

        public BlockService(BlockRepository blockRepository, FollowRepository followRepository)
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
        ///
        public void createBlock(string sender, string receiver, string reason)
        {
            if (_followRepository.GetFollowersOf(sender)
                .Any(f => f.getReceiver() == receiver) == true || _followRepository.GetFollowingOf(receiver)
                .Any(f => f.getSender() == sender) == true)
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
    }
}
