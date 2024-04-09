using System;
using UBB_SE_2024_Gaborment.Server.Mocks;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal class TrendingFeed : FeedConfiguration
    {
        int LikeCount { get; set; }
        int ViewCount { get; set; }
        int CommentCount { get; set; }

        public TrendingFeed(int LikeCount, int ViewCount, int CommentCount)
        {
            this.LikeCount = LikeCount;
            this.ViewCount = ViewCount;
            this.CommentCount = CommentCount;
        }

        public TrendingFeed()
        {
            this.LikeCount = new int();
            this.ViewCount = new int();
            this.CommentCount = new int();
        }

        private int GetPopularityProportion(int desiredValue, int actualValue)
        {
            if(actualValue >= (desiredValue * 3/4))
            {
                return 3;
            }
            else if(actualValue >= desiredValue/4)
            {
                return 1;
            }
            else
            {
                return 0;
            } 
        }

        public override int GetPostScore(PostMock post)
        {
            int positiveReactions = post.GetReactions()["like"] + post.GetReactions()["love"];
            int negativeReactions = post.GetReactions()["dislike"] + post.GetReactions()["angry"];
            if (negativeReactions > positiveReactions)
            {
                return 0;
            }

            int score = 0;
            score += GetPopularityProportion(LikeCount, positiveReactions);
            score += GetPopularityProportion(ViewCount, post.GetViews());
            score += GetPopularityProportion(CommentCount, post.GetNumberOfComments());

            return score;
        }
    } 
}
