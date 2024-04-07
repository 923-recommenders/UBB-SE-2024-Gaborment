﻿using UBB_SE_2024_Gaborment.Server.Mocks;
using UBB_SE_2024_Gaborment.Server.Relationships.Block;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;

namespace UBB_SE_2024_Gaborment.Server.LoggerUtils;

internal class ApplicationService
{
    private readonly Logger logger;
    private readonly FollowService followService;
    private readonly BlockService blockService;
    private readonly UserServiceMock userServiceMock;
    public ApplicationService()
    {
        logger = new Logger(true);
        FollowRepository followRepository = new FollowRepository();
        BlockRepository blockRepository= new BlockRepository();
        followService = new FollowService(blockRepository,followRepository);
        blockService = new BlockService(blockRepository, followRepository);
        userServiceMock = new UserServiceMock();
    }
}