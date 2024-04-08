using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
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

        public List<CustomFeed> GetFeedList(string userId)
        {
            return this.applicationContext.GetFeedList(userId);
        }

        public CustomFeed GetFeed(string userId, int searchedId)
        {
            return this.applicationContext.GetFeed(userId, searchedId);
        }

        public void AddCustomFeed(string userId, List<string>? hashtagsList, List<string>? locations, List<string>? users)
        {
            this.applicationContext.AddCustomFeedToRepository(userId, hashtagsList, locations, users);
        }

        public void UpdateCustomFeed(string userId, int id, List<string>? hashtagsList, List<string>? locations, List<string>? users)
        {
            this.applicationContext.UpdateCustomFeed(userId, id, hashtagsList, locations, users);
        }

        public void DeleteCustomFeed(string userId, int id)
        {
            this.applicationContext.DeleteCustomFeed(userId, id);
        }
    }
}
