using recommenders_backend.Relationships.Block;
using recommenders_backend.Relationships.Follow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Request
{
    internal class RequestTests
    {
        public static void TestRequestClass()
        {
            // Test Request class constructor
            Request request = new Request("sender1", "receiver1");
            if (request.getSender() != "sender1" || request.getReceiver() != "receiver1")
                Console.WriteLine("Request constructor failed.");
        }

        public static void TestRequestRepositoryClass()
        {
            // Creating a RequestRepository instance
            RequestRepository requestRepository = new RequestRepository();

            // Test addRequest and getRequest functions
            Request request1 = new Request("sender1", "receiver1");
            requestRepository.addRequest(request1);
            if (requestRepository.getRequestsOf("sender1").Count != 1 || requestRepository.getRequestsTo("receiver1").Count != 1)
                Console.WriteLine("addRequest or getRequest failed.");
            Request getRequest = requestRepository.getRequest("sender1", "receiver1");
            if (getRequest == null || getRequest.getSender() != "sender1" || getRequest.getReceiver() != "receiver1")
                Console.WriteLine("getRequest failed.");

            // Test removeRequest function
            requestRepository.removeRequest("sender1", "receiver1");
            if (requestRepository.getRequestsOf("sender1").Count != 0 || requestRepository.getRequestsTo("receiver1").Count != 0)
                Console.WriteLine("removeRequest failed.");

            // Test getRequestsOf function
            requestRepository.addRequest(new Request("sender2", "receiver1"));
            List<Request> requestsOfSender2 = requestRepository.getRequestsOf("sender2");
            if (requestsOfSender2.Count != 1 || requestsOfSender2[0].getReceiver() != "receiver1")
                Console.WriteLine("getRequestsOf failed.");

            // Test getRequestsTo function
            List<Request> requestsToReceiver1 = requestRepository.getRequestsTo("receiver1");
            if (requestsToReceiver1.Count != 1 || requestsToReceiver1[0].getSender() != "sender2")
                Console.WriteLine("getRequestsTo failed.");
        }

        public static void TestRequestServiceClass()
        {
            // Creating mock repositories and services
            RequestRepository requestRepository = new RequestRepository();
            FollowService followService = new FollowService(new BlockRepository(), new FollowRepository());
            BlockService blockService = new BlockService(new BlockRepository(), new FollowRepository());
            RequestService requestService = new RequestService(requestRepository, followService, blockService);

            // Test createRequest function
            requestService.createRequest("sender1", "receiver1");
            if (requestRepository.getRequestsOf("sender1").Count != 1 || requestRepository.getRequestsTo("receiver1").Count != 1)
                Console.WriteLine("createRequest failed.");

            // Test removeRequest function
            requestService.removeRequest("sender1", "receiver1");
            if (requestRepository.getRequestsOf("sender1").Count != 0 || requestRepository.getRequestsTo("receiver1").Count != 0)
                Console.WriteLine("removeRequest failed.");

            // Test getRequestOf function
            requestRepository.addRequest(new Request("sender2", "receiver1"));
            List<Request> requestsOfSender2 = requestService.getRequestOf("sender2");
            if (requestsOfSender2.Count != 1 || requestsOfSender2[0].getReceiver() != "receiver1")
                Console.WriteLine("getRequestOf failed.");

            // Test getRequestTo function
            List<Request> requestsToReceiver1 = requestService.getRequestTo("receiver1");
            if (requestsToReceiver1.Count != 1 || requestsToReceiver1[0].getSender() != "sender2")
                Console.WriteLine("getRequestTo failed.");

            // Test getAllRequests function
            Dictionary<string, List<Request>> allRequests = requestService.getAllRequests();
            if (!allRequests.ContainsKey("sender2") || allRequests["sender2"].Count != 1 || allRequests["sender2"][0].getReceiver() != "receiver1")
                Console.WriteLine("getAllRequests failed.");
        }
    }
}
