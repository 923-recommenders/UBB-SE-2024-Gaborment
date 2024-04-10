using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBB_SE_2024_Gaborment.Server.FeedConfigurations;
using UBB_SE_2024_Gaborment.Server.Mocks;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal class HomeFeed:FeedConfiguration
    {
        List<string> Locations { get; set; }
        List<string> FollowedUsers { get; set; }
        List<string> FrequentMedia { get; set; }
        List<string> Hashtags { get; set; }
        List<string> CloseFriends { get; set; }
        

        public HomeFeed(List<string> Locations, List<string> FollowedUsers,
            List<string> FrequentMedia, List<string> Hashtags, List<string> CloseFriends)
        {
            this.Locations = Locations;
            this.FollowedUsers = FollowedUsers;
            this.FrequentMedia = FrequentMedia;
            this.Hashtags = Hashtags;
        }

        public HomeFeed(List<string> CloseFriends)
        {
            this.CloseFriends = CloseFriends;
            this.Locations = new List<string>();
            this.FollowedUsers = new List<string>();
            this.FrequentMedia = new List<string>();
            this.Hashtags = new List<string>();
        }

        public override int GetPostScore(PostMock post)

        {
            int score = 0;

            foreach(string hashtag in post.GetHashtags())
            {
                if (Hashtags.Contains(hashtag))
                {
                    score += 1;
                }
            }

            if (FollowedUsers.Contains(post.GetOwner().username))
            {
                if(CloseFriends.Contains(post.GetOwner().username))
                {
                    score += 3;
                }
                else
                {
                    score += 1;
                }
            }

            if (FrequentMedia.Contains(post.GetMediaType()))
            {
                score += 1;
            }

            if (Locations.Contains(post.GetLocation()))
            {
                score += 1;
            }

            score += post.GetReactions().Values.Sum();

            return score;
        }

    }
}
