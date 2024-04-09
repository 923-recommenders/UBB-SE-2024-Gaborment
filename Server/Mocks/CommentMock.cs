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
        public string Text { get; set; }

        public CommentMock(UserMock Owner, string Text)
        {
            this.Owner = Owner;
            this.Text = Text;
        }

        public CommentMock()
        {
            this.Owner = null;
            this.Text = "";
        }

        public string getOwner()
        {
            return Owner.username;
        }

        public string getText()
        {
            return Text;
        }
    }
}
