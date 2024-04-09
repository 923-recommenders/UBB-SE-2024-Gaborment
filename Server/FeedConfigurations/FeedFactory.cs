using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal class FeedFactory
    {
        FollowService followService;
        List<string> closeFriends;

        public FeedFactory(FollowService followService)
        {
            this.followService = followService;
        }

        public FeedConfiguration CreateFeed(FeedTypes feedType, string user)
        {
            if (feedType == FeedTypes.ControversialFeed)
            {
                return new ControversialFeed();
            }
            else if (feedType == FeedTypes.FollowingFeed)
            {
                return new FollowingFeed();
            }
            else if (feedType == FeedTypes.TrendingFeed)
            {
                return new TrendingFeed();
            }
            else if (feedType == FeedTypes.HomeFeed)
            {
                closeFriends = followService.getCloseFriendsOf(user);
                return new HomeFeed(closeFriends);
            }
            else if (feedType == FeedTypes.CustomFeed)
            {
                return new CustomFeedBuilder(user);
            }
            else
            {
                throw new ArgumentException("Invalid feed type");
            }
        }
    }
}
