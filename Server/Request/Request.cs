namespace UBB_SE_2024_Gaborment.Server.Request
{
    internal class Request
    {
        private string sender;
        private string receiver;

        public string getSender()
        {
            return sender;
        }

        public string getReceiver()
        {
            return receiver;
        }

        public Request(string sender, string receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
        }
    }
}
