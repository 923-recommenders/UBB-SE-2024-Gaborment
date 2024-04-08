using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBB_SE_2024_Gaborment.Server.Relationships.Block;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal class FeedFactory
    {
        FollowService followService;
        List<String> closeFriends;

        public FeedFactory(FollowService followService)
        {
            this.followService = followService;
        }

        public FeedConfiguration CreateFeed(string feedType, string user)
        {
            if (feedType == "Controversial")
            {
                return new ControversialFeed();
            }
            else if (feedType == "Following")
            {
                return new FollowingFeed();
            }
            else if (feedType == "Trending")
            {
                return new TrendingFeed();
            }
            else if (feedType == "Home")
            {
                closeFriends = followService.getCloseFriendsOf(user);
                return new HomeFeed(closeFriends);
            }
            else if (feedType == "Custom")
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
