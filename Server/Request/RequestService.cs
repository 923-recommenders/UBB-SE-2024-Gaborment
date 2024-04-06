namespace UBB_SE_2024_Gaborment.Server.Request
{
    internal class RequestService
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private RequestRepository _requestRepository;
        private FollowService _followService;
        private BlockService _blockService;

        public RequestService(RequestRepository _requestRepository, FollowService _followService, BlockService _blockService)
        {
            this._requestRepository = _requestRepository;
            this._blockService = _blockService;
            this._followService = _followService;
        }

        RequestRepository getRequestRepository()
        {
            return _requestRepository;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void createRequest(string sender, string receiver)
        {
            //check if the set sender-receiver is located in a follow or a block from up until now. If it is, then the operation is not completed
            if (!(_blockService.getBlocksBy(sender).Any(b => b.getReceiver() == receiver) || _blockService.getBlocksOf(receiver).Any(b => b.getSender() == sender) || _followService.getFollowersOf(sender).Any(f => f.getReceiver() == receiver)))
            {
                Request requestToBeAdded = new Request(sender, receiver);
                _requestRepository.addRequest(requestToBeAdded);
            }
        }

        public void removeRequest(string sender, string receiver)
        {
            _requestRepository.removeRequest(sender, receiver);
        }

        public List<Request> getRequestOf(string sender)
        {
            return _requestRepository.getRequestsOf(sender);
        }

        public List<Request> getRequestTo(string receiver)
        {
            return _requestRepository.getRequestsTo(receiver);
        }

        public Dictionary<string, List<Request>> getAllRequests()
        {
            return _requestRepository.getRequestToDictionary();
        }
    }
}
