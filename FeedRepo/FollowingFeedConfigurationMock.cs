using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Gaborment.FeedRepo;

namespace UBB_SE_2024_Gaborment.FeedRepo

{
    public class FollowingFeedConfiguration : FeedConfiguration
    {
        



        public FollowingFeedConfiguration()
        {
        }

        public override Posts[] filterPosts(Posts[] posts)
        {
            return new Posts[0];

        }

        private List<Posts> FilterPostsByUserFollowing(List<Posts> posts)
        {
           
            return new List<Posts>();
        }

        public override int sortComparisonFunction(Posts Post1, Posts Post2)
        {
           return 1;
        }
    }
}
