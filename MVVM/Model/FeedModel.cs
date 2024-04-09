using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Gaborment.Server.Mocks;

namespace UBB_SE_2024_Gaborment.MVVM.Model
{
    internal class FeedModel
    {
        public string feedId { get; set; }
        public string feedName { get; set; }

        public List<PostMock> posts { get; set; }
    }
}
