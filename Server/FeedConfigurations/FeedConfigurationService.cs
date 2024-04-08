using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Gaborment.Server.FeedConfigurations;


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

        public List<CustomFeed> GetFeedList()
        {
            return this.applicationContext.GetFeedList();
        }

        public CustomFeed GetFeed(int searchedId)
        {
            return this.applicationContext.GetFeed(searchedId);
        }

        public void AddCustomFeed(List<string>? hashtagsList, List<string>? locations, List<string>? users)
        {
            this.applicationContext.AddCustomFeedToRepository(hashtagsList, locations, users);
        }

        public void UpdateCustomFeed(int id, List<string>? hashtagsList, List<string>? locations, List<string>? users)
        {
            this.applicationContext.UpdateCustomFeed(id, hashtagsList, locations, users);
        }

        public void DeleteCustomFeed(int id)
        {
            this.applicationContext.DeleteCustomFeed(id);
        }
    }
}
