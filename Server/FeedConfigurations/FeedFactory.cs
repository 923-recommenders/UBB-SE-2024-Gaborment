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
        User user;
        List<String> closeFriends;

        public FeedFactory(FollowService followService, User user)
        {
            this.followService = followService;
            this.user = user;
            this.closeFriends = followService.getCloseFriendsOf(user.GetUsername());
        }

        public FeedConfiguration CreateFeed(string feedType)
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
                return new HomeFeed(closeFriends);
            }
            else if (feedType == "Custom")
            {
                return new CustomFeedBuilder();
            }
            else
            {
                throw new ArgumentException("Invalid feed type");
            }
        }
    }
}
