using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeedConfigurations.Mocks;
using FeedConfigurations.Feeds;
using UBB_SE_2024_Gaborment.FeedConfigurations;


namespace FeedConfigurations
{
    internal class FeedTesting
    {
        public void TestCreationOfFeeds()
        {
            FeedFactory feedFactory = new FeedFactory();

            CustomFeedBuilder feedBuilder = (CustomFeedBuilder)feedFactory.CreateFeed("Custom");
            List<String> hashtags = new List<String>();
            hashtags.Add("hashtag1");
            hashtags.Add("hashtag2");
            List<String> locations = new List<String>();
            locations.Add("location1");

            feedBuilder.SetHashtags(hashtags);
            feedBuilder.SetLocations(locations);

            CustomFeed feed1 = feedBuilder.Build();
            Console.WriteLine($"\nFeed ID for CustomFeed (supposed to be 0): {feed1.GetID()}");
            Console.WriteLine("Hashtags for CustomFeed (supposed to be \"hashtag1 hashtag2\"): ");
            foreach(String hashtag in feed1.Hashtags)
            {
                Console.Write(hashtag);
                Console.Write(" ");
            }

            Console.WriteLine("\nLocations for CustomFeed (supposed to be \"location1\"): ");
            foreach(String location in feed1.Locations)
            {
                Console.Write(location);
                Console.Write(" ");
            }
            Console.WriteLine();


            FeedConfiguration feed2 = new ControversialFeed();
            feed2.SetID(1);
            feed2.SetName("Controversial");
            Console.WriteLine($"\nID for the Controversial feed (supposed to be 1): {feed2.GetID()}");
            Console.WriteLine($"Name for the Controversial feed (supposed to be \"Controversial\"): {feed2.GetName()}\n");
            

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FeedTesting feedTesting = new FeedTesting();
            feedTesting.TestCreationOfFeeds();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadLine();
        }
    }
}
