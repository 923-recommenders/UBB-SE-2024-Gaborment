using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeedConfigurations.Mocks;

namespace FeedConfigurations.Feeds
{
    internal class CustomFeed:FeedConfiguration
    {
        public List<String> Hashtags { get; set; }
        public List<String> Locations { get; set; }
        public List<String> FollowedUsers { get; set; }

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
