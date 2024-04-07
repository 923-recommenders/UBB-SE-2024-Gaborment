using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBB_SE_2024_Gaborment.FeedConfigurations;

using UBB_SE_2024_Gaborment.FeedConfigurations.Moks;

namespace UBB_SE_2024_Gaborment.FeedConfigurations
{
    public class FollowingFeed : FeedConfiguration
    {
        public List<String> FollowedUsers { get; set; }

        public FollowingFeed(List<String> usernames)
        {
            this.FollowedUsers = usernames;
        }

        public FollowingFeed()
        {
            this.FollowedUsers = new List<String>();
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