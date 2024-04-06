
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.FeedRepo
{
    public class HomePageFeedBuilderMock
    {
        public HomePageFeedBuilderMock() { }
        public FeedConfiguration Builder() { return new FollowingFeedConfiguration(); }
    }
}
