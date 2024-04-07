using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBB_SE_2024_Gaborment.FeedConfigurations;

using UBB_SE_2024_Gaborment.FeedConfigurations.Moks;

namespace UBB_SE_2024_Gaborment.FeedConfigurations
{
    internal class HomeFeed : FeedConfiguration
    {
        List<String> Locations { get; set; }
        List<String> FollowedUsers { get; set; }
        List<String> FrequentMedia { get; set; }
        List<String> Hashtags { get; set; }


        public HomeFeed(List<String> Locations, List<String> FollowedUsers,
            List<String> FrequentMedia, List<String> Hashtags)
        {
            this.Locations = Locations;
            this.FollowedUsers = FollowedUsers;
            this.FrequentMedia = FrequentMedia;
            this.Hashtags = Hashtags;
        }

        public HomeFeed()
        {
            this.FollowedUsers = new List<String>();
            this.Locations = new List<String>();
            this.FrequentMedia = new List<String>();
            this.Hashtags = new List<String>();
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