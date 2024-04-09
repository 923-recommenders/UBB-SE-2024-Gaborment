using System.Diagnostics;
using UBB_SE_2024_Gaborment.Database;
using UBB_SE_2024_Gaborment.Server.FollowSuggestions;
using UBB_SE_2024_Gaborment.Server.Mocks;
using UBB_SE_2024_Gaborment.Server.Relationships.Block;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;
using UBB_SE_2024_Gaborment.Server.Request;
using UBB_SE_2024_Gaborment.Server.FeedConfigurations;
using Microsoft.VisualBasic.ApplicationServices;


namespace UBB_SE_2024_Gaborment.Server.LoggerUtils;

internal class ApplicationService
{
    private static ApplicationService _instance;
    private readonly Logger logger;
    private readonly FollowService followService;
    private readonly BlockService blockService;
    private readonly RequestService requestService;
    private readonly UserServiceMock userServiceMock;
    private readonly FeedService feedService;
    private readonly FollowSuggestionEngine followSuggestionEngine;
    private readonly PostServiceMock postServiceMock;
    private readonly PostRepositoryMock postRepositoryMock;
    private readonly FeedConfigurationService feedConfigurationService;

    private ApplicationService()
    {
        logger = new Logger(true);
        ApplicationDatabaseContext applicationDatabaseContext = new ApplicationDatabaseContext();
        RequestRepository requestRepository = new RequestRepository(applicationDatabaseContext);
        FollowRepository followRepository = new FollowRepository(applicationDatabaseContext);
        followRepository.GetFollowers().ForEach(follower => Console.WriteLine(follower.getSender(), follower.getReceiver()));
        BlockRepository blockRepository = new BlockRepository(applicationDatabaseContext);
        followService = new FollowService(blockRepository, followRepository);
        blockService = new BlockService(blockRepository, followRepository);
        requestService = new RequestService(requestRepository, followService, blockService);
        userServiceMock = new UserServiceMock();
        followSuggestionEngine = new FollowSuggestionEngine(
            followService,
            blockService,
            requestService,
            userServiceMock,
            logger
        );
        postRepositoryMock = new PostRepositoryMock();
        postServiceMock = new PostServiceMock(postRepositoryMock, followService);
        feedConfigurationService = new FeedConfigurationService();
        feedService = new FeedService(postServiceMock,feedConfigurationService,followService);
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

    public List<PostMock> getFeedConfiguredPosts(string idUser, string idFeed)
    {
        DateTime startDate = DateTime.Now.AddHours(-3);
        DateTime endDate = DateTime.Now;
        
        return postServiceMock.searchVisiblePosts(idUser, startDate, endDate);

    }

    public List<FeedConfigurationDetails> getFeedConfigurationDetailsForUser(string userId)
    {
        return feedService.getFeedConfigurationDetailsForUser(userId);
    }

}
