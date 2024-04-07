using System;
using System.Collections.Generic;
using UBB_SE_2024_Gaborment.FeedConfigurations.Moks;


namespace UBB_SE_2024_Gaborment.FeedConfigurations
{
    internal class CustomFeedBuilder : FeedConfiguration
    {
        protected CustomFeed customFeed;

        public CustomFeedBuilder()
        {
            customFeed = new CustomFeed();
        }

        public CustomFeedBuilder SetHashtags(List<String> newHashtagList)
        {
            customFeed.Hashtags = newHashtagList;
            return this;
        }

        public CustomFeedBuilder SetLocations(List<String> newLocationList)
        {
            customFeed.Locations = newLocationList;
            return this;
        }

        public CustomFeedBuilder SetFollowedUsers(List<String> newFollowedUsersList)
        {
            customFeed.FollowedUsers = newFollowedUsersList;
            return this;
        }
        public CustomFeed Build()
        {
            return customFeed;
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