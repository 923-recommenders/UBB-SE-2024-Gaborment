
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
     internal class FeedConfigurationRepository
 {
     private Dictionary<string, Dictionary<int, CustomFeed>> userFeeds = new Dictionary<string, Dictionary<int, CustomFeed>>();
     private string filePath;

        [Serializable]
        public class CustomFeedWrapper
        {
            public int ID { get; set; }
            public string UserID { get; set; }
            public string Name { get; set; }
            public List<string> Hashtags { get; set; }
            public List<string> Locations { get; set; }
            public List<string> FollowedUsers { get; set; }
        }

        public FeedConfigurationRepository()
     {
         filePath = GenerateDefaultFilePath();

         if (File.Exists(filePath))
         {
             LoadFeedsFromXML(filePath);
         }
         else
         {
             CreateNewXMLFile(filePath);
         }
     }

     public List<CustomFeed> GetFeedList(string userId)
     {
         if (userFeeds.ContainsKey(userId))
         {
             return userFeeds[userId].Values.ToList();
         }
         else
         {
             throw new Exception("no feeds found for the specified user.");
         }
     }

     public CustomFeed GetFeed(string userId, int searchedId)
     {
         if (userFeeds.ContainsKey(userId) && userFeeds[userId].ContainsKey(searchedId))
         {
             return userFeeds[userId][searchedId];
         }
         else
         {
             throw new Exception($"no feed found with id {searchedId} for the specified user.");
         }
     }

     public void AddCustomFeedToRepository(string userId, string name, List<string>? hashtagsList, List<string>? locations, List<string>? users)
     {
         if (!userFeeds.ContainsKey(userId))
         {
             userFeeds[userId] = new Dictionary<int, CustomFeed>();
             Console.WriteLine($"added a new user {userId}");
         }

         CustomFeedBuilder customFeedBuilder = new CustomFeedBuilder();
            if(name!=null) { customFeedBuilder.SetName(name); }
         if (hashtagsList != null)
             customFeedBuilder.SetHashtags(hashtagsList);
         if (locations != null)
             customFeedBuilder.SetLocations(locations);
         if (users != null)
             customFeedBuilder.SetFollowedUsers(users);
         CustomFeed newFeed = customFeedBuilder.Build();
         if (newFeed == null)
         {
             throw new ArgumentNullException("custom feed failed to be passed in the repository");
         }
         else
         {
             int newId = userFeeds[userId].Count > 0 ? userFeeds[userId].Keys.Max() + 1 : 1;
             newFeed.SetID(newId);
             userFeeds[userId].Add(newId, newFeed);
             SaveCustomFeedsToXML(filePath);
         }
     }

     public void UpdateCustomFeed(string userId, int id, List<string>? hashtagsList, List<string>? locations, List<string>? users)
     {
         if (userFeeds.ContainsKey(userId) && userFeeds[userId].ContainsKey(id))
         {
             CustomFeedBuilder customFeedBuilder = new CustomFeedBuilder();
             if (hashtagsList != null)
                 customFeedBuilder.SetHashtags(hashtagsList);
             if (locations != null)
                 customFeedBuilder.SetLocations(locations);
             if (users != null)
                 customFeedBuilder.SetFollowedUsers(users);
             CustomFeed updatedFeed = customFeedBuilder.Build();
             if (updatedFeed == null)
             {
                 throw new ArgumentNullException("custom feed failed to be updated in the repository");
             }
             else
             {
                 userFeeds[userId][id] = updatedFeed;
                 SaveCustomFeedsToXML(filePath);
             }
         }
         else
         {
             throw new Exception("feed not found for the specified user.");
         }
     }

     public void DeleteCustomFeed(string userId, int id)
     {
         if (userFeeds.ContainsKey(userId) && userFeeds[userId].ContainsKey(id))
         {
             userFeeds[userId].Remove(id);
             SaveCustomFeedsToXML(filePath);
         }
         else
         {
             throw new Exception("feed not found for the specified user.");
         }
     }

     private string GenerateDefaultFilePath()
     {
         return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CustomFeeds.xml");
     }

     private void CreateNewXMLFile(string filePath)
     {
         XDocument xdoc = new XDocument(new XElement("CustomFeeds"));
         xdoc.Save(filePath);
     }

        private void LoadFeedsFromXML(string filePath)
        {
            XDocument xdoc = XDocument.Load(filePath);
            XElement root = xdoc.Element("CustomFeeds");

            if (root.HasElements)
            {
                foreach (var feedElement in root.Elements("CustomFeed"))
                {
                    CustomFeedWrapper feedWrapper = new CustomFeedWrapper();
                    feedWrapper.ID = (int)feedElement.Attribute("FeedID");
                    feedWrapper.UserID = (string)feedElement.Attribute("UserID");
                    feedWrapper.Name = (string)feedElement.Attribute("Name");
                    feedWrapper.Hashtags = feedElement.Element("Hashtags").Elements("Hashtag").Select(e => e.Value).ToList();
                    feedWrapper.Locations = feedElement.Element("Locations").Elements("Location").Select(e => e.Value).ToList();
                    feedWrapper.FollowedUsers = feedElement.Element("FollowedUsers").Elements("User").Select(e => e.Value).ToList();

                    CustomFeed customFeed = new CustomFeed(feedWrapper.Hashtags, feedWrapper.Locations, feedWrapper.FollowedUsers, feedWrapper.UserID);
                    customFeed.SetID(feedWrapper.ID);
                    customFeed.Name = feedWrapper.Name;

                    string userId = feedWrapper.UserID;
                    if (!userFeeds.ContainsKey(userId))
                    {
                        userFeeds[userId] = new Dictionary<int, CustomFeed>();
                    }
                    userFeeds[userId].Add(customFeed.GetID(), customFeed);
                }
            }
        }

        public void SaveCustomFeedsToXML(string filePath)
        {
            if (!userFeeds.Any())
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
                root.RemoveAll();

                foreach (var userFeedsPair in userFeeds)
                {
                    foreach (var feedPair in userFeedsPair.Value)
                    {
                        CustomFeed customFeed = feedPair.Value;

                        CustomFeedWrapper feedWrapper = new CustomFeedWrapper
                        {
                            ID = customFeed.GetID(),
                            UserID = userFeedsPair.Key,
                            Name = customFeed.Name,
                            Hashtags = customFeed.Hashtags,
                            Locations = customFeed.Locations,
                            FollowedUsers = customFeed.FollowedUsers
                        };

                        XElement customFeedElement = new XElement("CustomFeed",
                            new XAttribute("FeedID", feedWrapper.ID),
                            new XAttribute("UserID", feedWrapper.UserID),
                            new XAttribute("Name", feedWrapper.Name),
                            new XElement("Hashtags", feedWrapper.Hashtags.Select(h => new XElement("Hashtag", h))),
                            new XElement("Locations", feedWrapper.Locations.Select(l => new XElement("Location", l))),
                            new XElement("FollowedUsers", feedWrapper.FollowedUsers.Select(u => new XElement("User", u)))
                        );

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
