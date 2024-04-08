using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Gaborment.FeedRepo;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurationService
{
    internal class FeedConfigurationService
    {
        private FeedConfigurationRepository applicationContext;

        public FeedConfigurationService()
        {
            this.applicationContext = new FeedConfigurationRepository();
        }

        public FeedConfigurationService(FeedConfigurationRepository applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public List<FeedConfiguration> getFeedList()
        {
            return this.applicationContext.GetFeedList();
        }

        public FeedConfiguration getFeed(int id)
        {
            return this.applicationContext.getFeed(id);
        }

        public void addCustomFeed(string[]? hashtagsList, string[]? locations, string[]? users)
        {
            this.applicationContext.addCustomFeedToRepository(hashtagsList, locations, users);
        }

        public void updateCustomFeed(int id, string[]? hashtagsList, string[]? locations, string[]? users)
        {
            this.applicationContext.updateCustomFeed(id, hashtagsList, locations, users);
        }

        public void deleteCustomFeed(int id)
        {
            this.applicationContext.deleteCustomFeed(id);
        }
    }
}
