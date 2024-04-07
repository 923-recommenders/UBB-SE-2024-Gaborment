using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBB_SE_2024_Gaborment.FeedConfigurations;
using FeedConfigurations.Mocks;

namespace FeedConfigurations.Feeds
{
    internal class HomeFeed:FeedConfiguration
    {
        List<String> Locations { get; set; }
        List<String> FollowedUsers { get; set; }
        List<String> FrequentMedia { get; set; }
        List<String> Hashtags { get; set; }
        List<String> CloseFriends { get; set; }
        

        public HomeFeed(List<String> Locations, List<String> FollowedUsers,
            List<String> FrequentMedia, List<String> Hashtags, List<String> CloseFriends)
        {
            this.Locations = Locations;
            this.FollowedUsers = FollowedUsers;
            this.FrequentMedia = FrequentMedia;
            this.Hashtags = Hashtags;
        }

        public HomeFeed(List<String> CloseFriends)
        {
            this.CloseFriends = CloseFriends;
            this.Locations = new List<String>();
            this.FollowedUsers = new List<String>();
            this.FrequentMedia = new List<String>();
            this.Hashtags = new List<String>();
        }


        public override int GetPostScore(Post post)
        {
            int score = 0;

            foreach(String hashtag in post.GetHashtags())
            {
                if (Hashtags.Contains(hashtag))
                {
                    score += 1;
                }
            }

            if (FollowedUsers.Contains(post.GetOwner()))
            {
                if(CloseFriends.Contains(post.GetOwner()))
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

            score += (post.GetReactions().Values.Sum() / ReactionThreshold);

            return score;
        }

    }
}
