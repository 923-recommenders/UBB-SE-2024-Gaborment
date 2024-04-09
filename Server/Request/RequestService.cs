using UBB_SE_2024_Gaborment.Server.Mocks;
using UBB_SE_2024_Gaborment.Server.Relationships.Block;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;

namespace UBB_SE_2024_Gaborment.Server.Request
{
    internal class RequestService
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private RequestRepository _requestRepository;
        private FollowService _followService;
        private BlockService _blockService;
        private UserServiceMock _userServiceMock;
             

        public RequestService(RequestRepository _requestRepository, FollowService _followService, BlockService _blockService)
        {
            this._requestRepository = _requestRepository;
            this._blockService = _blockService;
            this._followService = _followService;
            _userServiceMock = new UserServiceMock();
    }

        public RequestService(RequestRepository _requestRepository, FollowService _followService, BlockService _blockService, UserServiceMock userService)
        {
            this._requestRepository = _requestRepository;
            this._blockService = _blockService;
            this._followService = _followService;
            this._userServiceMock = userService;
        }

        RequestRepository getRequestRepository()
        {
            return _requestRepository;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void createRequest(string sender, string receiver)
        {
            //!!!!!!!!!
            //check if the set sender-receiver is located in a follow or a block from up until now. If it is, then the operation is not completed
            if (!(_blockService.getBlocksBy(sender).Any(b => b.getReceiver() == receiver) || _blockService.getBlocksOf(receiver).Any(b => b.getSender() == sender) || _followService.getFollowersOf(sender).Any(f => f.getReceiver() == receiver) || _requestRepository.GetRequestsOf(sender).Any(r => r.getReceiver() == receiver)))
            {
                UserMock newUser = _userServiceMock.GetUserById(sender);
                if (newUser.isPublic == true)
                    _followService.createFollow(sender, receiver);
                else
                {
                    Request requestToBeAdded = new Request(sender, receiver);
                    _requestRepository.AddRequest(requestToBeAdded);
                }
                
            }
        }

        public void removeRequest(string sender, string receiver)
        {
            
            if (!(_blockService.getBlocksBy(sender).Any(b => b.getReceiver() == receiver) || _blockService.getBlocksOf(receiver).Any(b => b.getSender() == sender) || _followService.getFollowersOf(sender).Any(f => f.getReceiver() == receiver)))
            {
                _requestRepository.RemoveRequest(sender, receiver);
            }
        }

        public List<Request> getRequestOf(string sender)
        {
            return _requestRepository.GetRequestsOf(sender);
        }

        public List<Request> getRequestTo(string receiver)
        {
            return _requestRepository.GetRequestsTo(receiver);
        }

        public List<string> getRequestedUserIdsBy(string sender)
        {
            return _requestRepository.GetRequestsOf(sender).Select(request => request.getReceiver()).ToList();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        //!
        public List<UserMock> getRequestOfAsUserList(string sender)
        {
            List<string> requestList = _requestRepository.GetRequestsOf(sender).Select(request => request.getReceiver()).ToList();
            List<UserMock> followListUser = new List<UserMock>();
            foreach (string user in requestList)
            {
                UserMock FollowedUser = _userServiceMock.GetUserById(user);
                followListUser.Add(FollowedUser);
            }
            return followListUser;
        }

        //!
        public List<UserMock> getRequestToAsUserList(string receiver)
        {
            List<string> requestList = _requestRepository.GetRequestsTo(receiver).Select(request => request.getSender()).ToList();
            List<UserMock> followListUser = new List<UserMock>();
            foreach (string user in requestList)
            {
                UserMock FollowedUser = _userServiceMock.GetUserById(user);
                followListUser.Add(FollowedUser);
            }
            return followListUser;
        }
    }
}
