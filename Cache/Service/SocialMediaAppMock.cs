using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend
{
    public class SocialMediaAppMock
    {
        private readonly CacheModule<List<Post>> cache;
        private readonly CacheModule<Dictionary<string, User>> userCache;

        public SocialMediaAppMock()
        {
            cache = new CacheModule<List<Post>>();
            userCache = new CacheModule<Dictionary<string, User>>();
        }

        public void AddOrUpdatePostToFeed(string userId, Post post)
        {
            var userFeed = GetUserFeed(userId);
            userFeed.Add(post);
            UpdateUserFeedInCache(userId, userFeed);

        }


        public List<Post> GetUserFeed(string userId)
        {
            return GetUserFeedFromCache(userId);
        }

        public void FollowUser(string followerId, string userId)
        {
            var user = GetUserFromCache(followerId);
            if (user == null)
            {
                user = new User { Id = followerId };
            }
            user.Following.Add(userId);
            UpdateUserInCache(followerId, user);
        }

        public void UnfollowUser(string followerId, string userId)
        {
            var user = GetUserFromCache(followerId);
            if (user != null)
            {
                user.Following.Remove(userId);
                UpdateUserInCache(followerId, user);
            }
        }

        private bool postMatchesPreferences(Post post, List<string> preferences)
        {
            // check for example if tags of posts match preferences?
            foreach (var preference in preferences)
            {
                if (post.Content.Contains(preference))
                {
                    return true;
                }
            }
            return false;
        }

        private List<Post> GetUserFeedFromCache(string userId)
        {
            var userFeed = new List<Post>();
            var user = GetUserFromCache(userId);
            if (user != null)
            {
                foreach (var followerId in user.Following)
                {
                    var posts = cache.GetCache(followerId);
                    if (posts != null)
                    {
                        // Filter posts based on user preferences
                        //var filteredPosts = posts.Where(post => postMatchesPreferences(post, user.Preferences)).ToList();
                        //userFeed.AddRange(filteredPosts);
                        userFeed.AddRange(posts);
                    }
                }
                // Additionally, include the user's own posts
                var userPosts = cache.GetCache(userId);
                if (userPosts != null)
                {
                    // Filter user's own posts based on preferences
                    //var filteredUserPosts = userPosts.Where(post => postMatchesPreferences(post, user.Preferences)).ToList();
                    //userFeed.AddRange(filteredUserPosts);
                    userFeed.AddRange(userPosts);
                }
            }
            return userFeed;
        }

        /*private List<Post> GetUserFeedFromCache(string userId)
        {
            var userFeed = new List<Post>();
            var user = GetUserFromCache(userId);
            if (user != null)
            {
                foreach (var followerId in user.Following)
                {
                    var posts = _cache.GetCache(followerId);
                    if (posts != null)
                    {
                        userFeed.AddRange(posts);
                    }
                }
                // Additionally, include the user's own posts
                var userPosts = _cache.GetCache(userId);
                if (userPosts != null)
                {
                    userFeed.AddRange(userPosts);
                }
            }
            return userFeed;
        }*/

        private void UpdateUserFeedInCache(string userId, List<Post> userFeed)
        {
            cache.AddOrUpdateCache(userId, userFeed);
        }

        private User GetUserFromCache(string userId)
        {
            var users = userCache.GetCache("users") ?? new Dictionary<string, User>();
            if (users.TryGetValue(userId, out var user))
            {
                return user;
            }
            return null;
        }

        public void ClearUserFeed(string userId)
        {
            cache.ClearUserFeed(userId);
        }


        public void UpdateUserInCache(string userId, User user)
        {
            var users = userCache.GetCache("users") ?? new Dictionary<string, User>();
            users[userId] = user;
            userCache.AddOrUpdateCache("users", users);
        }

        public void printCache()
        {
            cache.PrintCacheContents();

        }

    }

}