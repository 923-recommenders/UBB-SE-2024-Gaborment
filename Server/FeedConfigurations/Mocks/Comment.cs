using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal class Comment
    {
        User Owner { get; set; }
        String Text { get; set; }

        public Comment(User Owner, String Text)
        {
            this.Owner = Owner;
            this.Text = Text;
        }

        public Comment()
        {
            this.Owner = null;
            this.Text = "";
        }

        public String getOwner()
        {
            return Owner.GetUsername();
        }

        public String getText()
        {
            return Text;
        }
    }
}
