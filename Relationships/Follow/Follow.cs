using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Relationships.Follow
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
        //ALSO ASSUMING THAT IF THE THERE IS NO DESCRIPTION THE STRING IS JUST EMPTY :)
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
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///Getters for interface users
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///tu ai pus setCloseFriend dar ma gandesc sa pun toggleCloseFriend pt a putea scoate userul de la close friend if needed
        public void toggleCloseFriend()
        {
            this.isCloseFriend = !this.isCloseFriend;
        }

        public void setExpirationTimeStamp(DateTime newExpirationTimeStamp)
        {
            this.expirationTimeStamp = newExpirationTimeStamp;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
