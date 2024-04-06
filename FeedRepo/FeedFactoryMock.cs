
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.FeedRepo
{
    public class FeedConfigurationFactoryMock
    {
        public FeedConfigurationFactoryMock() { }
        public FeedConfiguration makeCustomFeed(string[]? hashtagsList, string[]? locations, string[]? users) {
            return new FollowingFeedConfiguration();
        }
    }
}
