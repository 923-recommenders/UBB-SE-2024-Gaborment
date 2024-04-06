using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend.Relationships.Block
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
            return this.sender;
        }

        public string getReceiver()
        {
            return this.receiver;
        }

        public DateTime getStartingTimeStamp()
        {
            return this.startingTimeStamp;
        }

        public string getReason()
        {
            return this.reason;
        }

        
    }
}
