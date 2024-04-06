using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeedConfigurations.Feeds;

namespace UBB_SE_2024_Gaborment.FeedConfigurations
{
    public class FeedFactory
    {
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
                return new HomeFeed();
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
