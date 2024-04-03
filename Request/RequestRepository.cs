using recommenders_backend.Relationships.Follow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend.Request
{
    internal class RequestRepository
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private Dictionary<string, List<Request>> requestsFromDictionary;
        private Dictionary<string, List<Request>> requestsToDictionary;

        public RequestRepository()
        {
            requestsFromDictionary = new Dictionary<string, List<Request>>();
            requestsToDictionary = new Dictionary<string, List<Request>>();
        }
        
        public Dictionary<string, List<Request>> getRequestsFromDictionary()
        {
            return this.requestsFromDictionary;
        }

        public Dictionary<string, List<Request>> getRequestToDictionary()
        {
            return this.requestsToDictionary;
        }
        ///TODO CONSTRUCTORS FOR THE DB LIST

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        public void addRequest(Request request)
        {
            // Check if a list exists for the sender and the receiver. 
            // If it does not exist for any of them, create it
            if (!requestsFromDictionary.ContainsKey(request.getSender()))
            {
                requestsFromDictionary[request.getSender()] = new List<Request>();
            }
            if (!requestsToDictionary.ContainsKey(request.getReceiver()))
            {
                requestsToDictionary[request.getReceiver()] = new List<Request>();
            }

            // Check if the request already exists for the sender
            List<Request> senderFollows = requestsFromDictionary[request.getSender()];
            bool followExists = senderFollows.Any(f => f.getReceiver() == request.getReceiver());
            if (!followExists)
            {
                // Add it in the requestsFromDictionary at the sender key
                requestsFromDictionary[request.getSender()].Add(request);
                // Add it in the requestsToDictionary at the receiver key
                requestsToDictionary[request.getReceiver()].Add(request);
            }
        }

        public void removeRequest(string sender, string receiver)
        {
            // Check if the sender exists in followsFromDictionary
            if (requestsFromDictionary.ContainsKey(sender) && requestsToDictionary.ContainsKey(receiver))
            {
                List<Request> senderFollows = requestsFromDictionary[sender];

                // Check if there are follows from this sender to the receiver
                bool followExists = senderFollows.Any(f => f.getReceiver() == receiver);

                if (followExists)
                {
                    // Remove the Follow from the sender key from followsFromDictionary
                    requestsFromDictionary[sender].RemoveAll(f => f.getReceiver() == receiver);

                    // Remove the follow from the receiver key from followsToDictionary 
                    requestsToDictionary[receiver].RemoveAll(f => f.getSender() == sender);
                }
            }
        }

        public List<Request> getRequestsOf(string sender)
        {
            if (requestsFromDictionary.ContainsKey(sender))
            {
                return requestsFromDictionary[sender];
            }
            else
            {
                return new List<Request>(); // Return an empty list if no following found
            }
        }

        public List<Request> getRequestsTo(string receiver)
        {
            if (requestsToDictionary.ContainsKey(receiver))
            {
                return requestsToDictionary[receiver];
            }
            else
            {
                return new List<Request>(); // Return an empty list if no followers found
            }
        }

        public Request getRequest(string sender, string receiver)
        {
            if (requestsFromDictionary.ContainsKey(sender))
            {
                return requestsFromDictionary[sender].FirstOrDefault(f => f.getReceiver() == receiver);
            }
            else
            {
                return null; // Return null if the sender doesn't exist or no follow relationship found
            }
        }
    }
}
