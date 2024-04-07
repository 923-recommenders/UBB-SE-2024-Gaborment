﻿using UBB_SE_2024_Gaborment.Server.FollowSuggestions;
using UBB_SE_2024_Gaborment.Server.Mocks;
using UBB_SE_2024_Gaborment.Server.Relationships.Block;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;
using UBB_SE_2024_Gaborment.Server.Request;

namespace UBB_SE_2024_Gaborment.Server.LoggerUtils;

internal class ApplicationService
{
    private readonly Logger logger;
    private readonly FollowService followService;
    private readonly BlockService blockService;
    private readonly RequestService requestService;
    private readonly UserServiceMock userServiceMock;
    private readonly FollowSuggestionEngine followSuggestionEngine;
    public ApplicationService()
    {
        logger = new Logger(true);
        RequestRepository requestRepository = new RequestRepository();
        FollowRepository followRepository = new FollowRepository();
        BlockRepository blockRepository= new BlockRepository();
        followService = new FollowService(blockRepository,followRepository);
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
    }
}
