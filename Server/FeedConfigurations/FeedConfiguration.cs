﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBB_SE_2024_Gaborment.Server.Mocks;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal abstract class FeedConfiguration
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int _ReactionThreshold;
   
        public int GetID()
        {
            return ID;
        }
        public void SetID(int ID)
        {
            this.ID = ID;
        }

        public string GetName()
        {
            return Name;
        }

        public void SetName(string Name)
        {
            this.Name = Name;
        }
        public int GetReactionThreshold()
        {
            return ReactionThreshold;
        }

        public int ReactionThreshold
        {
            get { return _ReactionThreshold; }
            set
            {
                if (value >= 0)
                {
                    _ReactionThreshold = value;
                }
                else
                {
                    throw new ArgumentException("Reaction threshold must be a non-negative, valid number.");
                }
            }
        }

        public abstract int GetPostScore(PostMock post);

        public List<PostMock> FilterPosts(List<PostMock> posts)
        {
            List<PostMock> filteredPosts = new List<PostMock>();
            foreach (PostMock post in posts)
            {
                if (GetPostScore(post) > 0)
                {
                    filteredPosts.Add(post);
                }
            }
            return filteredPosts;
        }

        public virtual int SortComparisonFunction(PostMock Post1, PostMock Post2)
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
