
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UBB_SE_2024_Gaborment.Server.FeedConfigurations;

namespace UBB_SE_2024_Gaborment.FeedConfigurations.FeedConfigurationsRepository
{
    internal class FeedConfigurationRepository
    {
        private List<CustomFeed> feedList;
        private int freeId;

        public FeedConfigurationRepository()
        {
            freeId = 0;
            feedList = new List<CustomFeed>();
        }
        public List<CustomFeed> GetFeedList()
        {
            if (feedList != null)
                return feedList;
            else
                throw new Exception("the custom feed list is empty");
        }

        public CustomFeed GetFeed(int searchedId)
        {
            if (feedList == null)
            {
                throw new ArgumentNullException("the feed list from the repository is empty, no feed found");

            }
            else
            {
                CustomFeed searchedFeed = null!;

                var templateFeed = feedList
                                    .Where(feed => feed != null && feed.GetID() == searchedId)
                                    .SingleOrDefault();
                if (templateFeed != null)
                {
                    searchedFeed = templateFeed;
                }
                if (templateFeed == null)
                {
                    throw new ArgumentException($"no feed found with id {searchedId}");
                }

                if (searchedFeed == null)
                {
                    throw new ArgumentException($"no feed found with id {searchedId}");
                }
                return searchedFeed;
            }
        }

        public void AddCustomFeedToRepository(List<string>? hashtagsList, List<string>? locations, List<string>? users)
        {
            CustomFeedBuilder customFeedBuilder = new CustomFeedBuilder();
            if (hashtagsList != null)
                customFeedBuilder.SetHashtags(hashtagsList);
            if (locations != null)
                customFeedBuilder.SetLocations(locations);
            if (users != null)
                customFeedBuilder.SetFollowedUsers(users);
            CustomFeed newFeed = customFeedBuilder.Build();
            if (newFeed == null)
            {
                throw new ArgumentNullException("custom feed failed to be passed in the Repository");
            }
            else
            {
                feedList.Add(newFeed);
                newFeed.SetID(freeId);
                freeId++;
            }

        }
        public void UpdateCustomFeed(int id, List<string>? hashtagsList, List<string>? locations, List<string>? users)
        {
            FeedConfiguration toBeUpdatedFeed = GetFeed(id);
            if (toBeUpdatedFeed.GetType() == typeof(CustomFeed))
            {
                CustomFeedBuilder customFeedBuilder = new CustomFeedBuilder();
                if (hashtagsList != null)
                    customFeedBuilder.SetHashtags(hashtagsList);
                if (locations != null)
                    customFeedBuilder.SetLocations(locations);
                if (users != null)
                    customFeedBuilder.SetFollowedUsers(users);
                CustomFeed newFeed = customFeedBuilder.Build();
                toBeUpdatedFeed = newFeed;
            }
            else
            {
                throw new Exception("feed must be of type 'CUSTOM' in order to be updated from repo");
            }
        }

        public void DeleteCustomFeed(int id)
        {
            CustomFeed toBeDeletedFeed = GetFeed(id);
            if (toBeDeletedFeed.GetType() == typeof(CustomFeed))
            {
                feedList.Remove(toBeDeletedFeed);
                UpdateFeedIDs();
                freeId--;
            }
            else
            {
                throw new Exception("feed must be of type 'CUSTOM' in order to be deleted from repo");
            }
        }

        private int GetFreeFeedId()
        {
            return freeId;
        }


        private void UpdateFeedIDs()
        {
            for (int i = 0; i < feedList.Count; i++)
            {
                feedList[i].SetID(i);
            }
        }

        public void SaveCustomFeedsToXML(string filePath)
        {
            var customFeeds = feedList.OfType<CustomFeed>().ToList();
        
            if (!customFeeds.Any())
            {
                throw new InvalidOperationException("no CustomFeed objects to append.");
            }
        
            try
            {
                XDocument xdoc;
                if (File.Exists(filePath))
                {
                    xdoc = XDocument.Load(filePath);
                }
                else
                {
                    xdoc = new XDocument(new XElement("CustomFeeds"));
                }
        
                XElement root = xdoc.Element("CustomFeeds");
        
               
                HashSet<int> existingFeedIds = new HashSet<int>(
                    root.Elements("CustomFeed")
                        .Select(feedElement => (int)feedElement.Attribute("FeedID")));
        
                foreach (var customFeed in customFeeds)
                {
                   
                    if (!existingFeedIds.Contains(customFeed.GetID()))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(CustomFeed));
                        using (var stringWriter = new StringWriter())
                        {
                            serializer.Serialize(stringWriter, customFeed);
                            XElement customFeedElement = XElement.Parse(stringWriter.ToString());
                            
                            customFeedElement.SetAttributeValue("FeedID", customFeed.GetID());
                            root.Add(customFeedElement);
                        }
                    }
                    else
                    {
                        XElement existingFeedElement = root.Elements("CustomFeed")
                                                .FirstOrDefault(e => (int)e.Attribute("FeedID") == customFeed.GetID());
        
                        UpdateCustomFeedElement(existingFeedElement, customFeed);
                    }
                }
        
                xdoc.Save(filePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("an error occurred while saving to the XML file", ex);
            }
        }

        private void UpdateCustomFeedElement(XElement existingFeedElement, CustomFeed customFeed)
        {
            
            existingFeedElement.Element("Hashtags").ReplaceAll(customFeed.Hashtags.Select(h => new XElement("string", h)));
        
            
            existingFeedElement.Element("Locations").ReplaceAll(customFeed.Locations.Select(l => new XElement("string", l)));
        
           
            existingFeedElement.Element("FollowedUsers").ReplaceAll(customFeed.FollowedUsers.Select(u => new XElement("string", u)));
        }

    }
}
