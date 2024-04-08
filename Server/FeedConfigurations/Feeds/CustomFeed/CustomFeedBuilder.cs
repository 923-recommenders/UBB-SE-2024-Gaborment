using System;
using System.Collections.Generic;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal class CustomFeedBuilder:FeedConfiguration
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

    
        public override int GetPostScore(Post post)
        {
            throw new Exception("The feed builder does not have a score function.");
        }

    }
}
