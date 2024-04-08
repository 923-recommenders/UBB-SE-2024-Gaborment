using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBB_SE_2024_Gaborment.Server.FeedConfigurations;
using UBB_SE_2024_Gaborment.Server.Mocks;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal class CustomFeed:FeedConfiguration
    {
        public List<String> Hashtags { get; set; }
        public List<String> Locations { get; set; }
        public List<String> FollowedUsers { get; set; }
        public string User { get; set; }

        public CustomFeed(List<String> Hashtags, List<String> Locations, 
            List<String> FollowedUsers, string User)
        {
            this.Hashtags = Hashtags;
            this.Locations = Locations;
            this.FollowedUsers = FollowedUsers;
            this.User = User;
        }

        public CustomFeed()
        {
            this.Hashtags = new List<String>();
            this.Locations = new List<String>();
            this.FollowedUsers = new List<String>();
            this.User = "";
        }

        public CustomFeed(string User)
        {
            this.Hashtags = new List<String>();
            this.Locations = new List<String>();
            this.FollowedUsers = new List<String>();
            this.User = User;
        }

        public void SetUserID(string User)
        {
            this.User = User;
        }

        public string GetUserID()
        {
            return User;
        }

        public override int GetPostScore(PostMock post)
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
            if (FollowedUsers.Contains(post.GetOwner().username))
            {
                score += 3;
            }

            return score;
        }

    }
}
