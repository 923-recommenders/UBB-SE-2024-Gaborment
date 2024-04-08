using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Server.Mocks
{
    internal class CommentMock
    {
        public UserMock Owner { get; set; }
        public String Text { get; set; }

        public CommentMock(UserMock Owner, String Text)
        {
            this.Owner = Owner;
            this.Text = Text;
        }

        public CommentMock()
        {
            this.Owner = null;
            this.Text = "";
        }

        public String getOwner()
        {
            return Owner.username;
        }

        public String getText()
        {
            return Text;
        }
    }
}
