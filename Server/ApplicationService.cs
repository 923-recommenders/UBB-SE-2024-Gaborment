﻿using System.Diagnostics;
using UBB_SE_2024_Gaborment.Database;
using UBB_SE_2024_Gaborment.Server.FeedConfigurations;
using UBB_SE_2024_Gaborment.Server.FollowSuggestions;
using UBB_SE_2024_Gaborment.Server.Mocks;
using UBB_SE_2024_Gaborment.Server.Relationships.Block;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;
using UBB_SE_2024_Gaborment.Server.Request;
using UBB_SE_2024_Gaborment.Server.FeedConfigurations;
using Microsoft.VisualBasic.ApplicationServices;
using Bogus;
using UBB_SE_2024_Gaborment.Session;
using UBB_SE_2024_Gaborment.Server.Mocks.UserGeneration;
using System.Windows.Forms;


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
        RequestRepository requestRepository = new RequestRepository(applicationDatabaseContext, logger);
        FollowRepository followRepository = new FollowRepository(applicationDatabaseContext, logger);
        followRepository.GetFollowers().ForEach(follower => Console.WriteLine(follower.getSender(), follower.getReceiver()));
        BlockRepository blockRepository = new BlockRepository(applicationDatabaseContext, logger);
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

        int numberOfUserMock = 5;
        List<UserMock> userMocks = GenerateUsers.GenerateRandomUsers(numberOfUserMock);
        List<PostMock> postMocks = GeneratePosts.generateRandomPosts(20, userMocks);
        postRepositoryMock = new PostRepositoryMock(postMocks);
        postServiceMock = new PostServiceMock(postRepositoryMock, followService);
        feedConfigurationService = new FeedConfigurationService();
        feedService = new FeedService(postServiceMock, feedConfigurationService, followService);
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

    public List<PostMock> getFeedConfiguredPosts()
    {
        //This is for the basic case
        DateTime startDate = DateTime.Now.AddYears(-1);
        DateTime endDate = DateTime.Now;
        TimeSpan interval = TimeSpan.FromDays(3);
        TimeSpan limit = TimeSpan.FromDays(365 * 2);

        var session = ApplicationSession.Instance;
        //Temporary, then we use getfeedconfiguredposts for custom feed or getpostsforfeed
        //return postServiceMock.searchVisiblePosts(idUser, startDate, endDate);
        while (session.FeedStartTime > session.FeedEndTime-limit)
        {
            List<PostMock> list=feedService.getPostsForFeed(session.CurrentUserId, session.FeedStartTime, session.FeedEndTime, session.CurrentFeedConfiguration);
            if(list.Count > 0) { return list; }
            session.FeedStartTime = session.FeedStartTime - interval;

        }
        session.FeedEndTime = endDate;
        session.FeedStartTime = startDate;
        return feedService.getPostsForFeed(session.CurrentUserId, startDate, endDate, session.CurrentFeedConfiguration);
    }

    public void blockUser(string sender, string reciever, string reason)
    {
        blockService.createBlock(sender, reciever, reason);
    }

    public void removeBlock(string sender, string reciever)
    {
        blockService.RemoveBlock(sender, reciever);
    }

    public void unfollowUser(string sender, string reciever)
    {
        followService.removeFollow(sender, reciever);
    }

    public void acceptRequest(string sender, string reciever)
    {
        followService.createFollow(sender, reciever);
        requestService.removeRequest(reciever, sender);
    }

    public void deleteRequest(string sender, string reciever)
    {
        requestService.removeRequest(reciever, sender);
    }



    public List<FeedConfigurationDetails> getFeedConfigurationDetailsForUser(string userId)
    {
        return feedService.getFeedConfigurationDetailsForUser(userId);
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

    public List<UserMock> searchUsers(string searchToken)
    {
        return userServiceMock.searchUsers(searchToken);
    }
    public void addCustomFeed(string userId, string name, List<string> hashtagsList, List<string> locations, List<string> users)
    {
        feedConfigurationService.AddCustomFeed(userId,name, hashtagsList, locations, users);
    }

    public void updateCustomFeed(string userId, int customFeedId, List<string> hashtagsList, List<string> locations, List<string> users)
    {
        feedConfigurationService.UpdateCustomFeed(userId, customFeedId, hashtagsList, locations, users);
    }

    public void deleteCustomFeed(string userId, int customFeedId)
    {
        feedConfigurationService.DeleteCustomFeed(userId, customFeedId);
    }

    public List<FollowSuggestion> GetFollowSuggestionsForUser()
    {
        var session = ApplicationSession.Instance;
        return followSuggestionEngine.GetFollowSuggestionsForUser(session.CurrentUserId, AccountType.CASUAL_ACCOUNT);
    }
}

