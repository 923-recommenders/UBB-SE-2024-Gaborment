using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend.Relationships.Block
{
    internal class BlockRepository
    {

        private Dictionary<string, List<Block>> blockedByDictionary;
        public BlockRepository()
        {
            blockedByDictionary = new Dictionary<string, List<Block>>();
        }

        public Dictionary<string, List<Block>> getBlockedByDictionary() {
            return this.blockedByDictionary;
        }

        public void addBlock(Block blockToBeAdded)
        {
            if (!blockedByDictionary.ContainsKey(blockToBeAdded.getSender()))
            {
                blockedByDictionary[blockToBeAdded.getSender()] = new List<Block>();
            }

            List<Block> senderBlocks = blockedByDictionary[blockToBeAdded.getSender()];
            bool alreadyBlocked = senderBlocks.Exists(block => block.getReceiver() == blockToBeAdded.getReceiver());

            if (!alreadyBlocked)
            {
                senderBlocks.Add(blockToBeAdded);
            }
        }

        public void removeBlock(string sender, string receiver)
        {
            if (blockedByDictionary.ContainsKey(sender))
            {
                List<Block> senderBlocks = blockedByDictionary[sender];
                bool isBlocked = senderBlocks.Exists(block => block.getReceiver() == receiver);
                if (isBlocked == true)
                {
                    Block blockToRemove = senderBlocks.Find(block => block.getReceiver() == receiver);
                    senderBlocks.Remove(blockToRemove);
                }
            }
        }

        public List<Block> getBlocksBySender(string sender)
        {
            if (blockedByDictionary.ContainsKey(sender))
            {
                List<Block> senderBlocks = blockedByDictionary[sender];
                return senderBlocks; 
            }
            return new List<Block>();
        }

        
        public List<Block> getBlocksOfReceiver(string receiver)
        {

            List<Block> blocksOfReceiver = new List<Block>();

            foreach (var senderBlocks in blockedByDictionary.Values)
            {
                Block blockOfReceiver = senderBlocks.Find(potentialBlockOfReceiver => potentialBlockOfReceiver.getReceiver() == receiver);
                if (blockOfReceiver != null)
                {
                    blocksOfReceiver.Add(blockOfReceiver);
                }
            }

            return blocksOfReceiver;
        }

        
    }
}
