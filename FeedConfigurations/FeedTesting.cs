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
            Console.WriteLine("-------------------------Test Feed Creation--------------------------\n");
            FeedFactory feedFactory = new FeedFactory();

            CustomFeedBuilder feedBuilder = (CustomFeedBuilder)feedFactory.CreateFeed("Custom");
            List<String> hashtags = new List<String>
            {
                "hashtag1",
                "hashtag2"
            };

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
            Console.WriteLine($"Name for the Controversial feed (supposed to be \"Controversial\"): {feed2.GetName()}");
            Console.WriteLine("----------------------------------------------------------------------\n");

        }

        public void TestFilter()
        {
            Console.WriteLine("-------------------------Test Feed Filtering--------------------------\n");
            FeedFactory feedFactory = new FeedFactory();
            HomeFeed feed1 = (HomeFeed)feedFactory.CreateFeed("Home");
            feed1.SetReactionThreshold(2);

            User user1 = new User("User1");
            User user2 = new User("User2");
            User user3 = new User("User3");

            List<Post> posts = new List<Post>();

            Post post1 = new Post();
            post1.SetOwner(user1);
            post1.AddReaction("like", user1);

            Post post2 = new Post();
            post2.SetOwner(user1);
            post2.AddReaction("like", user2);
            post2.AddReaction("like", user3);

            Post post3 = new Post();
            post3.SetOwner(user2);
            post3.AddReaction("dislike", user1);

            posts.Add(post1);
            posts.Add(post2);
            posts.Add(post3);           

            List<Post> filteredPosts = feed1.FilterPosts(posts);
            foreach(Post post in filteredPosts)
            {
                Console.WriteLine("Post owner: " + post.GetOwner());
            }

            Console.WriteLine("Filtered posts (supposed to be 1): " + filteredPosts.Count);
            Console.WriteLine("------------------------------------------------------------- ---------\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FeedTesting feedTesting = new FeedTesting();
            feedTesting.TestCreationOfFeeds();
            feedTesting.TestFilter();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadLine();
        }
    }
}
