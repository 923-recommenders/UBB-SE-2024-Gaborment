using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend
{
    public class Post
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        public string toString() { string finalString = ""; finalString = finalString + "Id:" +Id+ "UserId" +UserId+ "Content:"+Content + "Timestamp:"+Timestamp; return finalString; }
    }
}