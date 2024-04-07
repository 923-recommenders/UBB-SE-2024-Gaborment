using System;
using UBB_SE_2024_Gaborment.FeedConfigurations.Moks;

namespace UBB_SE_2024_Gaborment.FeedConfigurations
{
    public class TrendingFeed : FeedConfiguration
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
            this.LikeCount = 0;
            this.ViewCount = 0;
            this.CommentCount = 0;
        }
        public override int SortComparisonFunction(Post Post1, Post Post2)
        {
            throw new NotImplementedException();
        }

        public override Post[] FilterPosts(Post[] posts)
        {
            throw new NotImplementedException();
        }
    }
}