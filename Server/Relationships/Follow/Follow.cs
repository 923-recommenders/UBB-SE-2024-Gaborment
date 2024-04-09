namespace UBB_SE_2024_Gaborment.Server.Relationships.Follow
{
    internal class Follow : Relationship
    {

        private string sender;
        private string receiver;
        private bool isCloseFriend;
        private DateTime expirationTimeStamp;
        private string description;

        public Follow(string sender, string receiver, string description)
        {
            this.sender = sender;
            this.receiver = receiver;
            isCloseFriend = false;
            expirationTimeStamp = DateTime.Now.AddYears(1);
            this.description = description;
        }
        public Follow(string sender, string receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
            isCloseFriend = false;
            expirationTimeStamp = DateTime.Now.AddYears(1);
            description = string.Empty;
        }

        public Follow(string sender, string receiver, DateTime expirationTimeStamp, string description)
        {
            this.sender = sender;
            this.receiver = receiver;
            isCloseFriend = false;
            this.expirationTimeStamp = expirationTimeStamp;
            this.description = description;
        }

        public Follow(string sender, string receiver,bool isCloseFriend, DateTime expirationTimeStamp, string description)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.isCloseFriend = isCloseFriend;
            this.expirationTimeStamp = expirationTimeStamp;
            this.description = description;
        }
        public Follow(string sender, string receiver, DateTime expirationTimeStamp)
        {
            this.sender = sender;
            this.receiver = receiver;
            isCloseFriend = false;
            this.expirationTimeStamp = expirationTimeStamp;
            description = string.Empty;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///Getters for interface users
        public string getSender()
        {
            return sender;
        }

        public string getReceiver()
        {
            return receiver;
        }

        public bool getCloseFriendStatus()
        {
            return isCloseFriend;
        }

        public DateTime getExpirationTimeStamp()
        {
            return expirationTimeStamp;
        }

        public string getDescription()
        {
            return description;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
         public void toggleCloseFriend()
        {
            isCloseFriend = !isCloseFriend;
        }

        public void setExpirationTimeStamp(DateTime newExpirationTimeStamp)
        {
            expirationTimeStamp = newExpirationTimeStamp;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
