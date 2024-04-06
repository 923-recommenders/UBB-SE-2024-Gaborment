using recommenders_backend.Relationships.Block;
using recommenders_backend.Relationships.Follow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend.Request
{
    internal class RequestTests
    {
        public static void TestRequestClass()
        {

            Request request = new Request("sender1", "receiver1");
            if (request.getSender() != "sender1" || request.getReceiver() != "receiver1")
                Console.WriteLine("Request constructor failed.");
        }

        public static void TestRequestRepositoryClass()
        {

            RequestRepository requestRepository = new RequestRepository();


            Request request1 = new Request("sender1", "receiver1");
            requestRepository.addRequest(request1);
            if (requestRepository.getRequestsOf("sender1").Count != 1 || requestRepository.getRequestsTo("receiver1").Count != 1)
                Console.WriteLine("addRequest or getRequest failed.");
            Request getRequest = requestRepository.getRequest("sender1", "receiver1");
            if (getRequest == null || getRequest.getSender() != "sender1" || getRequest.getReceiver() != "receiver1")
                Console.WriteLine("getRequest failed.");


            requestRepository.removeRequest("sender1", "receiver1");
            if (requestRepository.getRequestsOf("sender1").Count != 0 || requestRepository.getRequestsTo("receiver1").Count != 0)
                Console.WriteLine("removeRequest failed.");


            requestRepository.addRequest(new Request("sender2", "receiver1"));
            List<Request> requestsOfSender2 = requestRepository.getRequestsOf("sender2");
            if (requestsOfSender2.Count != 1 || requestsOfSender2[0].getReceiver() != "receiver1")
                Console.WriteLine("getRequestsOf failed.");


            List<Request> requestsToReceiver1 = requestRepository.getRequestsTo("receiver1");
            if (requestsToReceiver1.Count != 1 || requestsToReceiver1[0].getSender() != "sender2")
                Console.WriteLine("getRequestsTo failed.");
        }

        public static void TestRequestServiceClass()
        {

            RequestRepository requestRepository = new RequestRepository();
            FollowService followService = new FollowService(new BlockRepository(), new FollowRepository());
            BlockService blockService = new BlockService(new BlockRepository(), new FollowRepository());
            RequestService requestService = new RequestService(requestRepository, followService, blockService);


            requestService.createRequest("sender1", "receiver1");
            if (requestRepository.getRequestsOf("sender1").Count != 1 || requestRepository.getRequestsTo("receiver1").Count != 1)
                Console.WriteLine("createRequest failed.");


            requestService.removeRequest("sender1", "receiver1");
            if (requestRepository.getRequestsOf("sender1").Count != 0 || requestRepository.getRequestsTo("receiver1").Count != 0)
                Console.WriteLine("removeRequest failed.");


            requestRepository.addRequest(new Request("sender2", "receiver1"));
            List<Request> requestsOfSender2 = requestService.getRequestOf("sender2");
            if (requestsOfSender2.Count != 1 || requestsOfSender2[0].getReceiver() != "receiver1")
                Console.WriteLine("getRequestOf failed.");


            List<Request> requestsToReceiver1 = requestService.getRequestTo("receiver1");
            if (requestsToReceiver1.Count != 1 || requestsToReceiver1[0].getSender() != "sender2")
                Console.WriteLine("getRequestTo failed.");


            Dictionary<string, List<Request>> allRequests = requestService.getAllRequests();
            if (!allRequests.ContainsKey("sender2") || allRequests["sender2"].Count != 1 || allRequests["sender2"][0].getReceiver() != "receiver1")
                Console.WriteLine("getAllRequests failed.");
        }
    }
}
