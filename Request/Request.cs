﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Request
{
    internal class Request
    {
        private string sender;
        private string receiver;

        public string getSender()
        {
            return this.sender;
        }

        public string getReceiver()
        {
            return this.receiver;
        }

        public Request(string sender, string receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
        }
    }
}
