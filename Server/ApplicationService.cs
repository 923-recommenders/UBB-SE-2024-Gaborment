using System.Diagnostics;
using UBB_SE_2024_Gaborment.Database;
using UBB_SE_2024_Gaborment.Server.FollowSuggestions;
using UBB_SE_2024_Gaborment.Server.Mocks;
using UBB_SE_2024_Gaborment.Server.Relationships.Block;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;
using UBB_SE_2024_Gaborment.Server.Request;

namespace UBB_SE_2024_Gaborment.Server.LoggerUtils;

internal class ApplicationService
{
    private static ApplicationService _instance;
    private readonly Logger logger;
    private readonly FollowService followService;
    private readonly BlockService blockService;
    private readonly RequestService requestService;
    private readonly UserServiceMock userServiceMock;
    private readonly FollowSuggestionEngine followSuggestionEngine;

    private ApplicationService()
    {
        logger = new Logger(true);
        ApplicationDatabaseContext applicationDatabaseContext = new ApplicationDatabaseContext();
        RequestRepository requestRepository = new RequestRepository(applicationDatabaseContext);
        FollowRepository followRepository = new FollowRepository(applicationDatabaseContext);
        followRepository.GetFollowers().ForEach(follower => Console.WriteLine(follower.getSender(), follower.getReceiver()));
        BlockRepository blockRepository = new BlockRepository(applicationDatabaseContext);
        UserServiceMock userServiceMock = new UserServiceMock();    
        followService = new FollowService(blockRepository, followRepository, userServiceMock);
        blockService = new BlockService(blockRepository, followRepository, userServiceMock);
        requestService = new RequestService(requestRepository, followService, blockService, userServiceMock);
        userServiceMock = new UserServiceMock();
        followSuggestionEngine = new FollowSuggestionEngine(
            followService,
            blockService,
            requestService,
            userServiceMock,
            logger
        );
    }

    public static ApplicationService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ApplicationService();
            }
            return _instance;
        }
    }
    public List<UserMock> getRequestsUserSent(string userId)
    {
        return requestService.getRequestOfAsUserList(userId);
    }

    public List<UserMock> getRequestsUserReceived(string userId)
    {
        return requestService.getRequestToAsUserList(userId);
    }

    public List<UserMock> getPeopleUserIsFollowing(string userId) 
    {
        return followService.getFollowersOfAsUserList(userId);
    }

    public List<UserMock> getPeopleUserIsBeingFollowedBy(string userId)
    {
        return followService.getFollowingOfAsUserList(userId);
    }

    public List<UserMock> getAllCloseFriendsOfAnUser(string userId)
    {
        return followService.getCloseFriendsOfAsUserList(userId);
    }

    public List<UserMock> getPeopleUserBlocked(string userId)
    {
        return blockService.getBlocksByAsUserList(userId);
    }

    public List<UserMock> getPeopleUserIsBeingBlockedBy(string userId)
    {
        return blockService.getBlocksOfAsUserList(userId);
    }
}
