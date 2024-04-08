namespace UBB_SE_2024_Gaborment.Server.Relationships.Follow
{
    internal class Follow : Relationship
    {
        //note: these strings are actually guids
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///INITIALIZATION
        private string sender;
        private string receiver;
        private bool isCloseFriend;
        private DateTime expirationTimeStamp;
        private string description;


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///CONSTRUCTOR
        ///ASSUMING A FRIENDSHIP EXPIRES BY DEFAULT AFTER AN YEAR
        //ALSO ASSUMING THAT IF THE THERE IS NO DESCRIPTION THE string IS JUST EMPTY :)
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
        ///tu ai pus setCloseFriend dar ma gandesc sa pun toggleCloseFriend pt a putea scoate userul de la close friend if needed
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
