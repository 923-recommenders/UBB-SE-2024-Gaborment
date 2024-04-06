using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend.Relationships.Follow
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
            this.isCloseFriend = false;
            this.expirationTimeStamp = DateTime.Now.AddYears(1);
            this.description = description;
        }
        public Follow(string sender, string receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.isCloseFriend = false;
            this.expirationTimeStamp = DateTime.Now.AddYears(1);
            this.description = string.Empty;
        }

        public Follow(string sender, string receiver, DateTime expirationTimeStamp, string description)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.isCloseFriend = false;
            this.expirationTimeStamp = expirationTimeStamp;
            this.description = description;
        }
        public Follow(string sender, string receiver, DateTime expirationTimeStamp)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.isCloseFriend = false;
            this.expirationTimeStamp = expirationTimeStamp;
            this.description = string.Empty;
        }

        public string getSender()
        {
            return this.sender;
        }

        public string getReceiver()
        {
            return this.receiver;
        }

        public bool getCloseFriendStatus()
        {
            return this.isCloseFriend;
        }

        public DateTime getExpirationTimeStamp()
        {
            return this.expirationTimeStamp;
        }

        public string getDescription()
        {
            return this.description;
        }

       
        public void toggleCloseFriend()
        {
            this.isCloseFriend = !this.isCloseFriend;
        }

        public void setExpirationTimeStamp(DateTime newExpirationTimeStamp)
        {
            this.expirationTimeStamp = newExpirationTimeStamp;
        }

        
    }
}
