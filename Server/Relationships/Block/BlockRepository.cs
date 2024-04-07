namespace UBB_SE_2024_Gaborment.Server.Relationships.Block
{
    internal class BlockRepository
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///INITIALIZATION
        private Dictionary<string, List<Block>> blockedByDictionary;
        public BlockRepository()
        {
            blockedByDictionary = new Dictionary<string, List<Block>>();
        }

        public Dictionary<string, List<Block>> getBlockedByDictionary()
        {
            return blockedByDictionary;
        }

        ///TODOS - CONSTRUCTOR FRO ALREADY FORMED DICTIONARY (maybe checks?)

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        public void addBlock(Block blockToBeAdded)
        {
            // Check if there is a blocklist associated with the sender
            if (!blockedByDictionary.ContainsKey(blockToBeAdded.getSender()))
            {
                // If not, create a new blocklist for the sender
                blockedByDictionary[blockToBeAdded.getSender()] = new List<Block>();
            }

            // Check if there is not already a block on the receiver for this sender
            List<Block> senderBlocks = blockedByDictionary[blockToBeAdded.getSender()];
            bool alreadyBlocked = senderBlocks.Exists(block => block.getReceiver() == blockToBeAdded.getReceiver());

            if (!alreadyBlocked)
            {
                // Add the receiver to the sender's block list if not already blocked
                senderBlocks.Add(blockToBeAdded);
            }
        }

        public void removeBlock(string sender, string receiver)
        {
            if (blockedByDictionary.ContainsKey(sender))
            {
                //check if there is a block on the receiver from this sender
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

        //???
        public List<Block> getBlocksOfReceiver(string receiver)
        {

            //get all blocks from all all senders from this receiver
            List<Block> blocksOfReceiver = new List<Block>();

            foreach (var senderBlocks in blockedByDictionary.Values)
            {
                // Check if there are blocks from this sender to the specified receiver
                Block blockOfReceiver = senderBlocks.Find(potentialBlockOfReceiver => potentialBlockOfReceiver.getReceiver() == receiver);
                if (blockOfReceiver != null)
                {
                    // Add the block to the list if found
                    blocksOfReceiver.Add(blockOfReceiver);
                }
            }

            return blocksOfReceiver;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
