using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal abstract class FeedConfiguration
    {
        protected int ID { get; set; }
        protected String Name { get; set; }
        protected int ReactionThreshold { get; set; }
   
        public int GetID()
        {
            return ID;
        }
        public void SetID(int ID)
        {
            this.ID = ID;
        }

        public String GetName()
        {
            return Name;
        }

        public void SetName(String Name)
        {
            this.Name = Name;
        }
        public int GetReactionThreshold()
        {
            return ReactionThreshold;
        }

        public void SetReactionThreshold(int ReactionThreshold)
        {
            this.ReactionThreshold = ReactionThreshold;
        }

        public abstract int GetPostScore(Post post);

        public List<Post> FilterPosts(List<Post> posts)
        {
            List<Post> filteredPosts = new List<Post>();
            foreach (Post post in posts)
            {
                if (GetPostScore(post) > 0)
                {
                    filteredPosts.Add(post);
                }
            }
            return filteredPosts;
        }

        public virtual int SortComparisonFunction(Post Post1, Post Post2)
        {
            int score1 = GetPostScore(Post1);
            int score2 = GetPostScore(Post2);

            if (score1 > score2)
            {
                return -1;
            }
            else if (score1 < score2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }

}
