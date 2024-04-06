using recommenders_backend.Relationships.Follow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Relationships.Block
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
            return this._followRepository;
        }

        BlockRepository getBlockRepository()
        {
            return this._blockRepository;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        public void createBlock(string sender, string receiver, string reason)
        {
            if ((_followRepository.getFollowersOf(sender).Any(f => f.getReceiver() == receiver) == true) || (_followRepository.getFollowingOf(receiver).Any(f => f.getSender() == sender) == true))
            {
                _followRepository.removeFollow(sender, receiver);
            }
            Block blockToBeAdded = new Block(sender, receiver, DateTime.Now, reason);
            _blockRepository.addBlock(blockToBeAdded);
        }

        public void RemoveBlock(string sender, string receiver) 
        {
            _blockRepository.removeBlock(sender, receiver);
        }

        public List<Block> getBlocksBy(string sender) 
        {
            return _blockRepository.getBlocksBySender(sender);
        }

        public List<Block> getBlocksOf(string receiver)
        {
            return _blockRepository.getBlocksOfReceiver(receiver);
        }

        public Dictionary<string, List<Block>> getAllBlocks()
        {
            return _blockRepository.getBlockedByDictionary();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
