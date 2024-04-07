using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UBB_SE_2024_Gaborment.Server.Mocks
{
    internal class PostMock
    {
        Guid ID { get; set; }
        UserMock Owner { get; set; }
        String Text { get; set; }
        String Location { get; set; }
        String MediaType { get; set; } // a file
        List<String> Hashtags { get; set; }
        List<CommentMock> Comments { get; set; }
        int NumberOfViews { get; set; }
        Dictionary<String, List<UserMock>> ReactionsDictionary { get; set; }
        public DateTime PostingDate { get; set; }


        public PostMock(UserMock Owner, String Text, String Location,
            String MediaType, List<String> Hashtags, DateTime PostingDate)
        {
            this.ID = Guid.NewGuid();
            this.Owner = Owner;
            this.Text = Text;
            this.Location = Location;
            this.MediaType = MediaType;
            this.Hashtags = Hashtags;
            this.ReactionsDictionary = new Dictionary<String, List<UserMock>>();
            this.PostingDate = PostingDate; //TODO: give PostingDate as parameter or not
        }
        public PostMock()
        {
            this.ID = Guid.NewGuid();
            this.Owner = null;
            this.Text = "";
            this.Location = "";
            this.MediaType = "";
            this.Hashtags = new List<String>();
            this.ReactionsDictionary = new Dictionary<String, List<UserMock>>();
            this.PostingDate = DateTime.Now;
        }


        public void AddReaction(String reaction, UserMock user)
        {
            if (ReactionsDictionary.ContainsKey(reaction))
            {
                ReactionsDictionary[reaction].Add(user);
            }
            else
            {
                ReactionsDictionary.Add(reaction, new List<UserMock> { user });
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

        /*public String GetOwner()
        {
            return Owner.username;
        }*/

        public UserMock GetOwner()
        {
            return Owner;
        }

        public String GetUsernameOwner()
        {
            return Owner.username;
        }

        public void SetOwner(UserMock Owner)
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

        public List<CommentMock> GetComments()
        {
            return Comments;
        }

    }
}
