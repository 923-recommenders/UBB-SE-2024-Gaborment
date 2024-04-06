using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment
{
    public class CacheModule<T>
    {
        private readonly Dictionary<int, MemoryCache> cacheNodes;
        public List<string> keys;
        private readonly List<int> virtualNodes;
        private readonly int virtualNodeCount = 10; // Number of virtual nodes per cache node

        public CacheModule()
        {
            cacheNodes = new Dictionary<int, MemoryCache>();
            virtualNodes = new List<int>();
            keys = new List<string>();

            // Initialize cache nodes
            for (int i = 0; i < 3; i++) // Can change the number of cache nodes but for us i think even 1 is ok, but this is like the best tactic i found
            {
                var cacheNode = new MemoryCache(new MemoryCacheOptions());
                cacheNodes.Add(i, cacheNode);

                // Add virtual nodes
                for (int j = 0; j < virtualNodeCount; j++)
                {
                    int virtualNodeId = GetHash($"{i}-{j}"); // change hash function for something better
                    virtualNodes.Add(virtualNodeId);
                }
            }

            virtualNodes.Sort();
        }


        public void AddOrUpdateCache(string key, T value)
        {
            if(!keys.Contains(key))
                keys.Add(key);
            int hash = GetHash(key);
            int virtualNodeId = GetVirtualNodeId(hash);
            int cacheNodeId = GetCacheNodeId(virtualNodeId);

            // Set expiration policy: example expire after 5 seconds, but so we can see it work i set it to 0.003
            var expirationTime = TimeSpan.FromSeconds(0.003);
            cacheNodes[cacheNodeId].Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            });;
        }

        public void RemoveCache(string key)
        {
            int hash = GetHash(key);
            int virtualNodeId = GetVirtualNodeId(hash);
            int cacheNodeId = GetCacheNodeId(virtualNodeId);

            cacheNodes[cacheNodeId].Remove(key);
        }

        public void ClearUserFeed(string userId)
        {
            int hash = GetHash(userId);
            int virtualNodeId = GetVirtualNodeId(hash);
            int cacheNodeId = GetCacheNodeId(virtualNodeId);

            cacheNodes[cacheNodeId].Dispose();
            cacheNodes[cacheNodeId] = new MemoryCache(new MemoryCacheOptions());
        }

        public T GetCache(string key)
        {
            int hash = GetHash(key);
            int virtualNodeId = GetVirtualNodeId(hash);
            int cacheNodeId = GetCacheNodeId(virtualNodeId);

            return cacheNodes[cacheNodeId].Get<T>(key);
        }

        private int GetHash(string input)
        {
            // Implement a suitable hash function (e.g., CRC32, MD5, SHA-1)
            // For demonstration, we're using a simple hash function that returns the sum of ASCII values based on a random user string key
            int hash = 0;
            foreach (char c in input)
            {
                hash += (int)c;
            }
            return hash;
        }

        private int GetVirtualNodeId(int hash)
        {
            int index = virtualNodes.BinarySearch(hash);
            if (index < 0)
            {
                index = ~index; // Bitwise complement to get the index of the next higher node
                if (index >= virtualNodes.Count)
                {
                    index = 0; // Wrap around if the index exceeds the list size
                }
            }
            return virtualNodes[index];
        }

        private int GetCacheNodeId(int virtualNodeId)
        {
            // Simple modulo operation to map virtual nodes to cache nodes
            return virtualNodeId % cacheNodes.Count;
        }

        // this is only to see if everything is fine and does get evicted after a time, it won't remain
        public void PrintCacheContents()
        {
            Console.WriteLine("Cache Contents:");

            /*foreach (KeyValuePair<int, MemoryCache> elem in cacheNodes)
            {
                var first = elem.Key;
                int virtualNodeId = GetVirtualNodeId(first);
                int cacheNodeId = GetCacheNodeId(virtualNodeId);
                Console.WriteLine($"Key: {first}, Stuff: {cacheNodes[cacheNodeId].Get<T>("")}");
            }*/
                foreach (var key in keys)
                {
                    int hash = GetHash(key);
                    int virtualNodeId = GetVirtualNodeId(hash);
                    int cacheNodeId = GetCacheNodeId(virtualNodeId);

                    Console.WriteLine($"Key before hash: {key}, Key after hash: {cacheNodeId}, Stuff: {cacheNodes[cacheNodeId].Get<T>(key)}");

                }

        }

    }

}
