using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeedConfigurations.Mocks;

namespace FeedConfigurations.Feeds
{
    public abstract class FeedConfiguration
    {
        protected int ID { get; set; }
        protected String Name { get; set; }


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

        public abstract int SortComparisonFunction(Post Post1, Post Post2);
        public abstract Post[] FilterPosts(Post[] posts);
    }

}
