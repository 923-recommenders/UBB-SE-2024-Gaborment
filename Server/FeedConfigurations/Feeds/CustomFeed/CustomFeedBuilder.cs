using System;
using System.Collections.Generic;
using UBB_SE_2024_Gaborment.Server.Mocks;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal class CustomFeedBuilder:FeedConfiguration
    {
        protected CustomFeed customFeed;

        public CustomFeedBuilder()
        {
            customFeed = new CustomFeed();
        }

        public CustomFeedBuilder(string user)
        {
            customFeed = new CustomFeed(user);
        }


        public CustomFeedBuilder SetHashtags(List<string> newHashtagList)
        {
            customFeed.Hashtags = newHashtagList;
            return this; 
        }

        public CustomFeedBuilder SetLocations(List<string> newLocationList)
        {
            customFeed.Locations = newLocationList;
            return this; 
        }

        public CustomFeedBuilder SetFollowedUsers(List<string> newFollowedUsersList)
        {
            customFeed.FollowedUsers = newFollowedUsersList;
            return this; 
        }
        public CustomFeed Build()
        {
            return customFeed;
        }

    
        public override int GetPostScore(PostMock post)
        {
            throw new Exception("The feed builder does not have a score function.");
        }

    }
}
