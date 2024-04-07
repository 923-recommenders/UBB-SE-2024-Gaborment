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

        public CustomFeed(List<String> Hashtags, List<String> Locations, 
            List<String> FollowedUsers)
        {
            this.Hashtags = Hashtags;
            this.Locations = Locations;
            this.FollowedUsers = FollowedUsers;
        }

        public CustomFeed()
        {
            this.Hashtags = new List<String>();
            this.Locations = new List<String>();
            this.FollowedUsers = new List<String>();
        }

        public override int GetPostScore(Post post)
        {
            int score = 0;  

            // Filter by Locations
            if(Locations.Contains(post.GetLocation()))
            {
                score += 5;
            }

            // Filter by Matching Hashtags
            int hashtagNumber = Hashtags.Count;
            int hashtagMatch = 0;
            foreach(String hashtag in post.GetHashtags())
            {
                if (Hashtags.Contains(hashtag))
                {
                    hashtagMatch += 1;
                }
            }

            if(hashtagMatch == hashtagNumber)
            {
                score += 5;
            }else if(hashtagMatch >= 0.75 * hashtagNumber)
            {
                score += 3;
            }else if(hashtagMatch <= 0.5 * hashtagNumber && hashtagMatch > 0)
            {
                score += 1;
            }

            // Filter by Followed Users
            if (FollowedUsers.Contains(post.GetOwner()))
            {
                score += 3;
            }

            return score;
        }

    }
}
