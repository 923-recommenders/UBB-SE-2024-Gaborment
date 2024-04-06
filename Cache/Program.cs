using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading;

namespace recommenders_backend
{
    class Program
    {

        static void Main(string[] args)
        {
            /*var socialMediaApp = new SocialMediaAppMock();

            // Create users
            var user1 = new User { Id = "user1", Preferences = new List<string> { "technology", "programming" } }; // Manually input preferences for user1
            var user2 = new User { Id = "user2" };
            var user3 = new User { Id = "user3" };

            // Add users to cache
            socialMediaApp.UpdateUserInCache(user1.Id, user1);
            socialMediaApp.UpdateUserInCache(user2.Id, user2);
            socialMediaApp.UpdateUserInCache(user3.Id, user3);

            // User 1 follows User 2
            socialMediaApp.FollowUser(user1.Id, user2.Id);

            // Create posts
            var post1 = new Post { Id = "post1", UserId = user1.Id, Content = "Great article about new technology advancements!", Timestamp = DateTime.Now }; // Modify post content to match user1's preferences
            var post2 = new Post { Id = "post2", UserId = user2.Id, Content = "Nice day!", Timestamp = DateTime.Now };
            var post3 = new Post { Id = "post3", UserId = user3.Id, Content = "Greetings!", Timestamp = DateTime.Now };

            // Add posts to feed
            socialMediaApp.AddOrUpdatePostToFeed(user1.Id, post1);
            socialMediaApp.AddOrUpdatePostToFeed(user2.Id, post2);
            socialMediaApp.AddOrUpdatePostToFeed(user3.Id, post3);
            // Get user feed
            List<Post> user1Feed = socialMediaApp.GetUserFeed(user1.Id);

            // Display user feed
            Console.WriteLine("User 1 Feed:");
            foreach (var post in user1Feed)
            {
                Console.WriteLine($"Post ID: {post.Id}, Content: {post.Content}, Timestamp: {post.Timestamp}");
            }

            socialMediaApp.printCache();
            socialMediaApp.printCache();
            socialMediaApp.printCache();
            */


            // Clear user feed
            /*socialMediaApp.ClearUserFeed("user1");
            Console.WriteLine("User 1 Feed after clearing all cache:");
            // Fetch the now cleared cache for the feed (should be empty)
            List<Post> user1FeedAfterClear = socialMediaApp.GetUserFeed("user1");
            foreach (var post in user1FeedAfterClear)
            {
                Console.WriteLine($"Post ID: {post.Id}, Content: {post.Content}, Timestamp: {post.Timestamp}");
            }*/

            // functions for the MemoryCache
            /*
            var cacheModule = new CacheModule<string>();

            // Add some cache entries
            cacheModule.AddOrUpdateCache("user1", "John");
            cacheModule.AddOrUpdateCache("user2", "Alice");
            cacheModule.AddOrUpdateCache("user3", "Bob");

            // Print the initial cache contents
            cacheModule.PrintCacheContents();

            // Retrieve a cache entry
            var user2 = cacheModule.GetCache("user2");
            Console.WriteLine($"Retrieved from cache: {user2}");

            // Remove a cache entry
            //cacheModule.RemoveCache("user1");
            //var user1 = cacheModule.GetCache("user1");
            //Console.WriteLine($"Retrieved from cache: {user1}");

            // Print the updated cache contents
            cacheModule.PrintCacheContents();

            // Clear the cache for a specific user feed
            //cacheModule.ClearUserFeed("user3");

            // Print the final cache contents after clearing a user feed
            cacheModule.PrintCacheContents();
            
            */

            // Functions for the dictionary cache

            var cacheModule = new CacheModuleDictionaryBased<string>();

            // Add some data to the cache
            cacheModule.AddNewCacheOrUpdateExistingCache("key1", "value1");
            cacheModule.AddNewCacheOrUpdateExistingCache("key2", "value2");
            cacheModule.AddNewCacheOrUpdateExistingCache("key3", "value3");
            cacheModule.AddNewCacheOrUpdateExistingCache("key4", "value4");
            cacheModule.AddNewCacheOrUpdateExistingCache("key5", "value5");


            Console.WriteLine("Accessing cache data:");
            Console.WriteLine("Value for key1: " + cacheModule.GetCacheByKey("key1"));
            Console.WriteLine("Value for key1: " + cacheModule.GetCacheByKey("key1"));
            Console.WriteLine("Value for key1: " + cacheModule.GetCacheByKey("key1"));

            Console.WriteLine("Value for key2: " + cacheModule.GetCacheByKey("key2"));
            Console.WriteLine("Value for key2: " + cacheModule.GetCacheByKey("key2"));
            Console.WriteLine("Value for key2: " + cacheModule.GetCacheByKey("key2"));
            Console.WriteLine("Value for key2: " + cacheModule.GetCacheByKey("key2"));

            Console.WriteLine("Value for key3: " + cacheModule.GetCacheByKey("key3"));
            Console.WriteLine("Value for key3: " + cacheModule.GetCacheByKey("key3"));
            Console.WriteLine("Value for key3: " + cacheModule.GetCacheByKey("key3"));

            Console.WriteLine("Value for key2: " + cacheModule.GetCacheByKey("key4"));

            Console.WriteLine("Value for key3: " + cacheModule.GetCacheByKey("key5"));

            Console.WriteLine();

            /*
            cacheModule.RemoveCacheByKey("key1");
            cacheModule.RemoveCacheByKey("key2");
            cacheModule.RemoveCacheByKey("key3");
            cacheModule.RemoveCacheByKey("key4");
            cacheModule.RemoveCacheByKey("key5");
            */

            Console.WriteLine("Changing key3 with key 21");
            cacheModule.changeKeyButKeepCacheContents("key3", "key21");
            Console.WriteLine("Value for key21: " + cacheModule.GetCacheByKey("key21"));
            try
            {
                Console.WriteLine("Value for key3: " + cacheModule.GetCacheByKey("key3"));
            }
            catch (Exception e) {Console.WriteLine("Error: {0}",e.ToString()); }
            //try
            //{
            //    cacheModule.RemoveCacheByKey("key3");
            //}
            //catch (Exception e) { Console.WriteLine("Error: {0}", e.ToString()); }
            //Console.WriteLine("Value for key3: " + cacheModule.GetCacheByKey("key3"));
            //cacheModule.RemoveCacheByKey("key1");

            Console.WriteLine(cacheModule.PrintCacheContents());

            //Console.WriteLine("Value for key3: " + cacheModule.GetCacheByKey("key3"));

            int topCacheDataToPrint = 5;
            Console.WriteLine(cacheModule.PrintFrequentData(topCacheDataToPrint));
            //cacheModule.PrintFrequentData(topCacheDataToPrint);
            Console.ReadKey();
        }
    }
}