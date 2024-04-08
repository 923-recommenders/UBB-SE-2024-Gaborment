using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Gaborment.Server.Mocks;
using UBB_SE_2024_Gaborment.Server.FeedConfigurations;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;

namespace UBB_SE_2024_Gaborment.Server
{
    internal class FeedService
    {
        private PostServiceMock postsService;
        private FeedConfigurationService feedConfigurationService;
        private FollowService followService;
        private FeedFactory feedFactory;

        public List<PostMock> getPostsForFeed(string userId, FeedTypes feedType, DateTime startDate, DateTime endDate, int? feedId)
        {
            return;
        }

        public List<FeedConfigurationDetails> getFeedConfigurationDetailsForUser(string userId)
        {
            List<FeedConfigurationDetails> list = new List<FeedConfigurationDetails>();
            list.Add(new FeedConfigurationDetails("Home Feed", FeedTypes.HomeFeed, -1));
            list.Add(new FeedConfigurationDetails("Following Feed", FeedTypes.FollowingFeed, -1));
            list.Add(new FeedConfigurationDetails("Trending Feed", FeedTypes.TrendingFeed, -1));
            list.Add(new FeedConfigurationDetails("Controversial Feed", FeedTypes.ControversialFeed, -1));
            foreach(CustomFeed customFeed in this.feedConfigurationService.GetFeedList(userId))
            {
                list.Add(new FeedConfigurationDetails(customFeed.GetName(), FeedTypes.CustomFeed, customFeed.GetID()));
            }

            return list;
        }
    }
}
