namespace UBB_SE_2024_Gaborment.Server.Relationships.Block
{
    internal class Block : Relationship
    {
        private string sender { get; }
        private string receiver { get; }
        private DateTime startingTimeStamp;
        private string reason;



        public Block(string sender, string receiver, DateTime startingTimeStamp, string reason)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.startingTimeStamp = startingTimeStamp;
            this.reason = reason;
        }

        public string getSender()
        {
            return sender;
        }

        public string getReceiver()
        {
            return receiver;
        }

        public DateTime getStartingTimeStamp()
        {
            return startingTimeStamp;
        }

        public string getReason()
        {
            return reason;
        }

        
    }
}
