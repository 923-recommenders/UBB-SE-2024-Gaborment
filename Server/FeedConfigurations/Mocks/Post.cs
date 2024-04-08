using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal class Post
    {
        Guid ID { get; set; }
        User Owner { get; set; }
        String Text { get; set; }
        String Location { get; set; }
        String MediaType { get; set; } // a file
        List<String> Hashtags { get; set; }
        List<Comment> Comments { get; set; }
        int NumberOfViews { get; set; }
        Dictionary<String, List<User>> ReactionsDictionary { get; set; }


        public Post(User Owner, String Text, String Location, 
            String MediaType, List<String> Hashtags)
        {
            this.ID = Guid.NewGuid();
            this.Owner = Owner;
            this.Text = Text;
            this.Location = Location;
            this.MediaType = MediaType;
            this.Hashtags = Hashtags;
            this.ReactionsDictionary = new Dictionary<String, List<User>>();
        }
        public Post() 
        { 
            this.ID = Guid.NewGuid();
            this.Owner = null;
            this.Text = "";
            this.Location = "";
            this.MediaType = "";
            this.Hashtags = new List<String>();
            this.ReactionsDictionary = new Dictionary<String, List<User>>();
        }


        public void AddReaction(String reaction, User user)
        {
            if (ReactionsDictionary.ContainsKey(reaction))
            {
                ReactionsDictionary[reaction].Add(user);
            }
            else
            {
                ReactionsDictionary.Add(reaction, new List<User> { user });
            }
        }

        public List<String> GetHashtags()
        {
            return Hashtags;
        }

        public void setHashtags(List<String> Hashtags)
        {
            this.Hashtags = Hashtags;
        }

        public Guid GetID()
        {
            return ID;
        }

        public void SetID(Guid ID)
        {
            this.ID = ID;
        }

        public String GetOwner()
        {
            return Owner.GetUsername();
        }

        public void SetOwner(User Owner)
        {
            this.Owner = Owner;
        }


        public String GetText()
        {
            return Text;
        }

        public void SetText(String Text)
        {
            this.Text = Text;
        }

        public String GetMediaType()
        {
            return MediaType;
        }

        public void SetMediaType(String MediaType)
        {
            this.MediaType = MediaType;
        }

        public String GetLocation()
        {
            return Location;
        }

        public void SetLocation(String Location)
        {
            this.Location = Location;
        }

        public int GetViews()
        {
            return NumberOfViews;
        }

        public void SetNumberOfViews(int NumberOfViews)
        {
            this.NumberOfViews = NumberOfViews;
        }

        public Dictionary<String, int> GetReactions()
        {
            Dictionary<String, int> reactions = new Dictionary<String, int>();
            foreach (String reaction in ReactionsDictionary.Keys)
            {
                reactions.Add(reaction, ReactionsDictionary[reaction].Count);
            }
            return reactions;
        }

        public List<Comment> GetComments()
        {
            return Comments;
        }

    }
}
