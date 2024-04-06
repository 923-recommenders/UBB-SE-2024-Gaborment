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


        public void addRequest(Request request)
        {

            if (!requestsFromDictionary.ContainsKey(request.getSender()))
            {
                requestsFromDictionary[request.getSender()] = new List<Request>();
            }
            if (!requestsToDictionary.ContainsKey(request.getReceiver()))
            {
                requestsToDictionary[request.getReceiver()] = new List<Request>();
            }

            List<Request> senderFollows = requestsFromDictionary[request.getSender()];
            bool followExists = senderFollows.Any(f => f.getReceiver() == request.getReceiver());
            if (!followExists)
            {

                requestsFromDictionary[request.getSender()].Add(request);
                requestsToDictionary[request.getReceiver()].Add(request);
            }
        }

        public void removeRequest(string sender, string receiver)
        {

            if (requestsFromDictionary.ContainsKey(sender) && requestsToDictionary.ContainsKey(receiver))
            {
                List<Request> senderFollows = requestsFromDictionary[sender];


                bool followExists = senderFollows.Any(f => f.getReceiver() == receiver);

                if (followExists)
                {

                    requestsFromDictionary[sender].RemoveAll(f => f.getReceiver() == receiver);

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
                return new List<Request>(); 
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
                return new List<Request>(); 
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
                return null; 
            }
        }
    }
}
