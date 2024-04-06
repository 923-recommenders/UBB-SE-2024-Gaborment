using recommenders_backend.Relationships.Follow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend.Relationships.Block
{
    internal class BlockTests
    {
        public static void TestBlockClass()
        {
            DateTime timeStamp = DateTime.Now;
            
            Block block = new Block("senderId", "receiverId", timeStamp, "Test reason");
            if (block.getSender() != "senderId" || block.getReceiver() != "receiverId" || block.getReason() != "Test reason")
                Console.WriteLine("Block constructor failed.");

            
            if (block.getStartingTimeStamp() != timeStamp)
                Console.WriteLine("getStartingTimeStamp failed.");
        }

        public static void TestBlockRepositoryClass()
        {
            BlockRepository repository = new BlockRepository();

            
            Block block1 = new Block("sender1", "receiver1", DateTime.Now, "Reason 1");
            repository.addBlock(block1);
            if (repository.getBlocksBySender("sender1").Count != 1)
                Console.WriteLine("addBlock failed.");
            repository.removeBlock("sender1", "receiver1");
            if (repository.getBlocksBySender("sender1").Count != 0)
                Console.WriteLine("removeBlock failed.");


            Block block2 = new Block("sender2", "receiver1", DateTime.Now, "Reason 2");
            repository.addBlock(block2);
            List<Block> blocks = repository.getBlocksBySender("sender2");
            if (blocks.Count != 1 || blocks[0].getReceiver() != "receiver1")
                Console.WriteLine("getBlocksBySender failed.");


            List<Block> blocksOfReceiver = repository.getBlocksOfReceiver("receiver1");
            if (blocksOfReceiver.Count != 1 || blocksOfReceiver[0].getSender() != "sender2")
                Console.WriteLine("getBlocksOfReceiver failed.");
        }

        public static void TestBlockServiceClass()
        {

            FollowRepository followRepository = new FollowRepository();
            BlockRepository blockRepository = new BlockRepository();
            BlockService blockService = new BlockService(blockRepository, followRepository);


            blockService.createBlock("sender1", "receiver1", "Reason 1");
            if (blockRepository.getBlocksBySender("sender1").Count != 1 || blockRepository.getBlocksOfReceiver("receiver1").Count != 1)
                Console.WriteLine("createBlock failed.");


            blockService.RemoveBlock("sender1", "receiver1");
            if (blockRepository.getBlocksBySender("sender1").Count != 0 || blockRepository.getBlocksOfReceiver("receiver1").Count != 0)
                Console.WriteLine("RemoveBlock failed.");


            blockService.createBlock("sender1", "receiver1", "Reason 1");
            List<Block> senderBlocks = blockService.getBlocksBy("sender1");
            if (senderBlocks.Count != 1 || senderBlocks[0].getReceiver() != "receiver1")
                Console.WriteLine("getBlocksBy failed.");


            List<Block> receiverBlocks = blockService.getBlocksOf("receiver1");
            if (receiverBlocks.Count != 1 || receiverBlocks[0].getSender() != "sender1")
                Console.WriteLine("getBlocksOf failed.");


            Dictionary<string, List<Block>> allBlocks = blockService.getAllBlocks();
            if (!allBlocks.ContainsKey("sender1") || allBlocks["sender1"].Count != 1 || allBlocks["sender1"][0].getReceiver() != "receiver1")
                Console.WriteLine("getAllBlocks failed.");
        }
    }
}
