
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace UBB_SE_2024_Gaborment.FeedConfigurations
{
    public class FeedConfigurationRepository
    {
        private List<FeedConfiguration> feedList;
        private int freeId;

        public FeedConfigurationRepository()
        {
            FeedFactory feedFactory = new FeedFactory();

            feedList = new List<FeedConfiguration>();

            FeedConfiguration homeFeed = feedFactory.CreateFeed("Home");

            FeedConfiguration trendingFeed = feedFactory.CreateFeed("Trending");

            FeedConfiguration controversialFeed = feedFactory.CreateFeed("Controversial");

            FeedConfiguration followingFeed = feedFactory.CreateFeed("Following");

            feedList.Add(homeFeed);
            feedList.Add(trendingFeed);
            feedList.Add(controversialFeed);
            feedList.Add(followingFeed);

            freeId = 4;
        }
        public List<FeedConfiguration> GetFeedList()
        {
            return feedList;
        }

        public FeedConfiguration GetFeed(int searchedId)
        {
            if (feedList == null)
            {
                throw new ArgumentNullException("the feed list from the repository is empty, no feed found");

            }
            else
            {
                FeedConfiguration searchedFeed = null!;

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
            FeedConfiguration newFeed = customFeedBuilder.Build();
            if (newFeed == null)
            {
                throw new ArgumentNullException("custom feed failed to be passed in the Repository");
            }
            else
            {
                feedList.Add(newFeed);
                newFeed.SetID(freeId);
            }
            freeId++;
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
                FeedConfiguration newFeed = customFeedBuilder.Build();
                toBeUpdatedFeed = newFeed;
            }
            else
            {
                throw new Exception("feed must be of type 'CUSTOM' in order to be updated from repo");
            }
        }

        public void DeleteCustomFeed(int id)
        {
            FeedConfiguration toBeDeletedFeed = GetFeed(id);
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
                throw new InvalidOperationException("No CustomFeed objects to append.");
            }

            try
            {

                XDocument xdoc = File.Exists(filePath) ? XDocument.Load(filePath) : new XDocument();


                XElement root = xdoc.Element("CustomFeeds");
                if (root == null)
                {
                    root = new XElement("CustomFeeds");
                    xdoc.Add(root);
                }


                foreach (var customFeed in customFeeds)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CustomFeed));
                    using (var stringWriter = new StringWriter())
                    {
                        serializer.Serialize(stringWriter, customFeed);
                        XElement customFeedElement = XElement.Parse(stringWriter.ToString());
                        root.Add(customFeedElement);
                    }
                }

                xdoc.Save(filePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("an error occurred while saving to the XML file", ex);
            }

        }
    }
}
