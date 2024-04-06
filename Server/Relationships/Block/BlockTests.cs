using UBB_SE_2024_Gaborment.Server.Relationships.Follow;

namespace UBB_SE_2024_Gaborment.Server.Relationships.Block
{
    internal class BlockTests
    {
        public static void TestBlockClass()
        {
            DateTime timeStamp = DateTime.Now;
            // Test Block class constructor
            Block block = new Block("senderId", "receiverId", timeStamp, "Test reason");
            if (block.getSender() != "senderId" || block.getReceiver() != "receiverId" || block.getReason() != "Test reason")
                Console.WriteLine("Block constructor failed.");

            // Test getStartingTimeStamp
            if (block.getStartingTimeStamp() != timeStamp)
                Console.WriteLine("getStartingTimeStamp failed.");
        }

        public static void TestBlockRepositoryClass()
        {
            BlockRepository repository = new BlockRepository();

            // Test addBlock and removeBlock functions
            Block block1 = new Block("sender1", "receiver1", DateTime.Now, "Reason 1");
            repository.addBlock(block1);
            if (repository.getBlocksBySender("sender1").Count != 1)
                Console.WriteLine("addBlock failed.");
            repository.removeBlock("sender1", "receiver1");
            if (repository.getBlocksBySender("sender1").Count != 0)
                Console.WriteLine("removeBlock failed.");

            // Test getBlocksBySender function
            Block block2 = new Block("sender2", "receiver1", DateTime.Now, "Reason 2");
            repository.addBlock(block2);
            List<Block> blocks = repository.getBlocksBySender("sender2");
            if (blocks.Count != 1 || blocks[0].getReceiver() != "receiver1")
                Console.WriteLine("getBlocksBySender failed.");

            // Test getBlocksOfReceiver function
            List<Block> blocksOfReceiver = repository.getBlocksOfReceiver("receiver1");
            if (blocksOfReceiver.Count != 1 || blocksOfReceiver[0].getSender() != "sender2")
                Console.WriteLine("getBlocksOfReceiver failed.");
        }

        public static void TestBlockServiceClass()
        {
            // Creating mock repositories
            FollowRepository followRepository = new FollowRepository();
            BlockRepository blockRepository = new BlockRepository();
            BlockService blockService = new BlockService(blockRepository, followRepository);

            // Test createBlock function
            blockService.createBlock("sender1", "receiver1", "Reason 1");
            if (blockRepository.getBlocksBySender("sender1").Count != 1 || blockRepository.getBlocksOfReceiver("receiver1").Count != 1)
                Console.WriteLine("createBlock failed.");

            // Test RemoveBlock function
            blockService.RemoveBlock("sender1", "receiver1");
            if (blockRepository.getBlocksBySender("sender1").Count != 0 || blockRepository.getBlocksOfReceiver("receiver1").Count != 0)
                Console.WriteLine("RemoveBlock failed.");

            // Test getBlocksBy function
            blockService.createBlock("sender1", "receiver1", "Reason 1");
            List<Block> senderBlocks = blockService.getBlocksBy("sender1");
            if (senderBlocks.Count != 1 || senderBlocks[0].getReceiver() != "receiver1")
                Console.WriteLine("getBlocksBy failed.");

            // Test getBlocksOf function
            List<Block> receiverBlocks = blockService.getBlocksOf("receiver1");
            if (receiverBlocks.Count != 1 || receiverBlocks[0].getSender() != "sender1")
                Console.WriteLine("getBlocksOf failed.");

            // Test getAllBlocks function
            Dictionary<string, List<Block>> allBlocks = blockService.getAllBlocks();
            if (!allBlocks.ContainsKey("sender1") || allBlocks["sender1"].Count != 1 || allBlocks["sender1"][0].getReceiver() != "receiver1")
                Console.WriteLine("getAllBlocks failed.");
        }
    }
}
