using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Gaborment.Server.Mocks;
using UBB_SE_2024_Gaborment.Server.FeedConfigurations;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;
using UBB_SE_2024_Gaborment.Server.Sorting;

namespace UBB_SE_2024_Gaborment.Server
{
    internal class FeedService
    {
        private PostServiceMock postsService;
        private FeedConfigurationService feedConfigurationService;
        private FollowService followService;
        private FeedFactory feedFactory;
        private Logger logger;

        public FeedService(PostServiceMock postsService, FeedConfigurationService feedConfigurationService, FollowService followService, Logger logger)
        {
            this.postsService = postsService;
            this.feedConfigurationService = feedConfigurationService;
            this.followService = followService;
            this.feedFactory = new FeedFactory(followService);
            this.logger = logger;
        }

        public List<PostMock> getPostsForFeed(string userId, DateTime startDate, DateTime endDate, FeedConfigurationDetails feedConfigurationDetails)
        {
            List<PostMock> resultPosts = this.postsService.searchVisiblePosts(userId, startDate, endDate);
            FeedConfiguration feed;

            if(feedConfigurationDetails.feedType == FeedTypes.CustomFeed)
            {
                feed = this.feedConfigurationService.GetFeed(userId, feedConfigurationDetails.feedId);
            }
            else
            {
                feed = this.feedFactory.CreateFeed(feedConfigurationDetails.feedType, userId);
            }

            List<PostMock> filteredPosts = feed.FilterPosts(resultPosts);

            ISorter<PostMock> sorter = new MergeSort<PostMock>();
            Func<PostMock, PostMock, int> comparisonFunction = (post1, post2) => feed.SortComparisonFunction(post1, post2);

            sorter.SortAscending(filteredPosts, comparisonFunction);
            filteredPosts.Reverse();
            return filteredPosts;
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
