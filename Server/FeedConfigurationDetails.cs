using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Server
{
    public enum FeedTypes
    {
        CustomFeed,
        TrendingFeed,
        ControversialFeed,
        HomeFeed,
        FollowingFeed
    }

    internal class FeedConfigurationDetails
    {
        public FeedConfigurationDetails(string feedName, FeedTypes feedType, int feedId)
        {
            this.feedName = feedName;
            this.feedType = feedType;
            this.feedId = feedId;
        }

        public FeedConfigurationDetails()
        {
            this.feedName = "";
            this.feedType = FeedTypes.CustomFeed;
            this.feedId = -1;
        }

        public string feedName { get; set; }
        public FeedTypes feedType { get; set; }
        public int feedId {  get; set; }

    }
}
