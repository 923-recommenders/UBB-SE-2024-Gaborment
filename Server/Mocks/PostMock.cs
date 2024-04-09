using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UBB_SE_2024_Gaborment.Server.Mocks
{
    internal class PostMock : IComparable<PostMock>
    {
        Guid ID { get; set; }
        UserMock Owner { get; set; }
        string Text { get; set; }
        string Location { get; set; }
        string MediaType { get; set; } // a file
        List<string> Hashtags { get; set; }
        List<CommentMock> Comments { get; set; }
        int NumberOfComments { get; set; }
        int NumberOfViews { get; set; }
        Dictionary<string, List<UserMock>> ReactionsDictionary { get; set; }
        public DateTime PostingDate { get; set; }


        public PostMock(UserMock Owner, string Text, string Location,
            string MediaType, List<string> Hashtags, int NumberOfViews,
            int NumberOfComments, Dictionary<string, List<UserMock>> Reactions,
            DateTime PostingDate)
        {
            this.ID = Guid.NewGuid();
            this.Owner = Owner;
            this.Text = Text;
            this.Location = Location;
            this.MediaType = MediaType;
            this.Hashtags = Hashtags;
            this.NumberOfViews = NumberOfViews;
            this.NumberOfComments = NumberOfComments;
            this.ReactionsDictionary = Reactions;
            this.PostingDate = PostingDate;
        }
        public PostMock()
        {
            this.ID = Guid.NewGuid();
            this.Owner = null;
            this.Text = "";
            this.Location = "";
            this.MediaType = "";
            this.Hashtags = new List<string>();
            this.ReactionsDictionary = new Dictionary<string, List<UserMock>>();
            this.PostingDate = DateTime.Now;
        }


        public void AddReaction(string reaction, UserMock user)
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

        public List<string> GetHashtags()
        {
            return Hashtags;
        }

        public void setHashtags(List<string> Hashtags)
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

        public UserMock GetOwner()
        {
            return Owner;
        }

        public string GetUsernameOwner()
        {
            return Owner.username;
        }

        public void SetOwner(UserMock Owner)
        {
            this.Owner = Owner;
        }


        public string GetText()
        {
            return Text;
        }

        public void SetText(string Text)
        {
            this.Text = Text;
        }

        public string GetMediaType()
        {
            return MediaType;
        }

        public void SetMediaType(string MediaType)
        {
            this.MediaType = MediaType;
        }

        public string GetLocation()
        {
            return Location;
        }

        public void SetLocation(string Location)
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

        public int GetNumberOfComments()
        {
            return NumberOfComments;
        }

        public void SetNumberOfComments(int NumberOfComments)
        {
            this.NumberOfComments = NumberOfComments;
        }

        public Dictionary<string, int> GetReactions()
        {
            Dictionary<string, int> reactions = new Dictionary<string, int>();
            foreach (string reaction in ReactionsDictionary.Keys)
            {
                reactions.Add(reaction, ReactionsDictionary[reaction].Count);
            }
            return reactions;
        }

        public List<CommentMock> GetComments()
        {
            return Comments;
        }


        public int CompareTo(PostMock other)
        {
            if (other == null)
                return 1; // If the other object is null, this instance comes after it.

            // Compare by PostingDate
            return this.PostingDate.CompareTo(other.PostingDate);
        }
    }
}
