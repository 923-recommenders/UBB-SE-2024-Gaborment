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
        private Logger logger;

        public FeedConfigurationService(Logger logger)
        {
            this.applicationContext = new FeedConfigurationRepository();
            this.logger = logger;
        }

        public FeedConfigurationService(FeedConfigurationRepository applicationContext, Logger logger)
        {
            this.applicationContext = applicationContext;
            this.logger = logger;
        }

        public List<CustomFeed> GetFeedList(string userId)
        {
            try
            { 
                return this.applicationContext.GetFeedList(userId); 
            }
            catch (Exception ex)
            {
                this.logger.Log("ERROR", ex.Message);
                throw;
            }
        }

        public CustomFeed GetFeed(string userId, int searchedId)
        {
            try
            { 
                return this.applicationContext.GetFeed(userId, searchedId); 
            }
            catch (Exception ex)
            {
                this.logger.Log("ERROR", ex.Message);
                throw;
            }
        }

        public void AddCustomFeed(string userId, List<string>? hashtagsList, List<string>? locations, List<string>? users)
        {
            try
            { 
                this.applicationContext.AddCustomFeedToRepository(userId, hashtagsList, locations, users); 
            }
            catch (Exception ex)
            {
                this.logger.Log("ERROR", ex.Message);
                throw;
            }
        }

        public void UpdateCustomFeed(string userId, int id, List<string>? hashtagsList, List<string>? locations, List<string>? users)
        {
            try
            {
                this.applicationContext.UpdateCustomFeed(userId, id, hashtagsList, locations, users);
            }
            catch (Exception ex)
            {
                this.logger.Log("ERROR", ex.Message);
                throw;
            }
        }

        public void DeleteCustomFeed(string userId, int id)
        {
            try
            { 
                this.applicationContext.DeleteCustomFeed(userId, id); 
            }
            catch (Exception ex)
            {
                this.logger.Log("ERROR", ex.Message);
                throw;
            }
        }
    }
}
