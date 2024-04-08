using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBB_SE_2024_Gaborment.Server.FeedConfigurations;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal class FollowingFeed : FeedConfiguration
    {
        public List<String> FollowedUsers { get; set;}

        public FollowingFeed(List<String> usernames) { 
            this.FollowedUsers = usernames;
        }

        public FollowingFeed() { 
            this.FollowedUsers = new List<String>();
        }

        public override int GetPostScore(Post post)
        {
            int score = 0;
            if (FollowedUsers.Contains(post.GetOwner()))
            {
                score += 1;
            }
            return score;
        }

    }
}
