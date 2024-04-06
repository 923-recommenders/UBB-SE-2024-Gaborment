using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


//https://medium.com/@rinorhajrizi1718/designing-a-distributed-caching-mechanism-for-a-social-networking-app-f07c716856b2 - for the virtual node and consistent hash idea


namespace UBB_SE_2024_Gaborment
{
    public class CacheData<T>
    {
        public T data { get; set; }
        public int frequency { get; set; }

        
    }

    public class CacheModuleDictionaryBased<T>
    {
        private readonly int capacity;
        private readonly List<int> virtualNodes;
        private readonly int virtualNodeCount;
        private readonly int cacheNodesCount;
        //private readonly Dictionary<int, (T value, int frequency)> cacheNodes;
        private readonly Dictionary<int, CacheData<T>> cacheNodes;
        //private readonly SortedDictionary<int, HashSet<int>> frequenciesAndAssociatedHashValues;
        private readonly SortedDictionary<int, HashSet<int>> frequenciesAndAssociatedHashValues;

        public CacheModuleDictionaryBased()
        {
            virtualNodes = new List<int>();
            //cacheNodes = new Dictionary<int, (T, int)>();
            cacheNodes = new Dictionary<int, CacheData<T>>();
            frequenciesAndAssociatedHashValues = new SortedDictionary<int, HashSet<int>>();
            capacity = 1000;
            virtualNodeCount = 50;
            cacheNodesCount = 20;

            // Initialize cache nodes and virtual nodes to handle 100 elements for starters
            for (int i = 0; i < cacheNodesCount; i++)
            {
                // Add virtual nodes
                for (int j = 0; j < virtualNodeCount; j++)
                {
                    int virtualNodeId = GetHashForKey($"{i}.{j}");
                    virtualNodes.Add(virtualNodeId); // Add the virtual node ID to the list so we can look through them whenever we want to add a new cache node
                    var data = new CacheData<T> { data = default, frequency = 0 }; // Manually input preferences for user1
                    cacheNodes[virtualNodeId] = data; // Initialize cache node with default value and zero frequency to avoid any conflicts
                }
            }
            virtualNodes.Sort();
        }

        public void AddNewCacheOrUpdateExistingCache(string key, T value)
        {
            // Check if the cacheNodes dictionary has reached its capacity
            if (cacheNodes.Count() > capacity)
            {
                // Remove least frequent elements in cache to make space for the new addition
                RemoveLeastFrequentNodes();
            }

            int hash = GetHashForKey(key);
            int virtualNodeId = GetVirtualNodeIdFromHash(hash);

            var cachedData = cacheNodes[virtualNodeId];
            cachedData.data = value;
            cacheNodes[virtualNodeId] = cachedData; // Increase frequency by 1
            //var (cachedValue, frequency) = cacheNodes[virtualNodeId];
            //cacheNodes[virtualNodeId] = (value, frequency + 1); // Increase frequency by 1

            // Update frequency using the hash of the actual cache key
            UpdateFrequency(hash, 1);

        }

        public void changeKeyButKeepCacheContents(string oldKey, string newKey)
        {
            int oldHash = GetHashForKey(oldKey);
            int oldVirtualNodeId = GetVirtualNodeIdFromHash(oldHash);
            if (!cacheNodes.ContainsKey(oldVirtualNodeId))
                throw new Exception($"Key '{oldKey}' doesn't exist in the cache!");
            //var (cachedValue, frequency) = cacheNodes[oldVirtualNodeId];
            var cacheData = cacheNodes[oldVirtualNodeId];
            //cacheNodes.Remove(oldVirtualNodeId);
            RemoveCacheByKey(oldKey);
            int newHash = GetHashForKey(newKey);
            int newVirtualNodeId = GetVirtualNodeIdFromHash(newHash);
            //cacheNodes[newVirtualNodeId] = (cachedValue, frequency);
            cacheNodes[newVirtualNodeId] = cacheData;

            //UpdateFrequency(newHash, cacheData.frequency); // Update the frequency using the new hash value
        }



        private void RemoveLeastFrequentNodes()
        {
            // Determine the number of nodes to remove to maintain the capacity
            int nodesToRemove = Math.Max(cacheNodes.Count() - capacity, 0);

            // Frequencies staring from the smallest to the largest for ease of deleting
            var frequencies = frequenciesAndAssociatedHashValues.Keys.OrderBy(freq => freq).ToList();

            // Remove nodes with the smallest frequencies
            foreach (var freq in frequencies)
            {
                var hashSetCopy = new HashSet<int>(frequenciesAndAssociatedHashValues[freq]);

                foreach (var hash in hashSetCopy)
                {
                    // Remove the node from cacheNodes and its corresponding frequency set
                    cacheNodes.Remove(hash);
                    frequenciesAndAssociatedHashValues[freq].Remove(hash);
                    nodesToRemove--;
                    if (nodesToRemove <= 0)
                    {
                        return;
                    }
                }
            }
        }

        public void RemoveCacheByKey(string key) {
            int oldHash = GetHashForKey(key);
            int oldVirtualNodeId = GetVirtualNodeIdFromHash(oldHash);
            if (!cacheNodes.ContainsKey(oldVirtualNodeId))
                throw new Exception($"Key '{key}' doesn't exist in the cache!");
            //var(cachedValue, frequency) = cacheNodes[oldVirtualNodeId];
            var cacheData = cacheNodes[oldVirtualNodeId];
            cacheNodes.Remove(oldVirtualNodeId);

            // Remove the hash from its corresponding frequency key as it shouldn't exist anymore
            foreach (var keyValuePair in frequenciesAndAssociatedHashValues)
            {
                if (keyValuePair.Value.Contains(oldHash))
                {
                    keyValuePair.Value.Remove(oldHash);
                    break;
                }
            }
        }

        public T GetCacheByKey(string key)
        {
            int hash = GetHashForKey(key);
            int virtualNodeId = GetVirtualNodeIdFromHash(hash);

            if (!cacheNodes.ContainsKey(virtualNodeId))
                throw new Exception($"Cache doesn't contain key: {key}!");

            UpdateFrequency(hash, 1);

            return cacheNodes[virtualNodeId].data;
        }

        private int GetHashForKey(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToInt32(bytes, 0);
            }
        }


        private int GetVirtualNodeIdFromHash(int hash)
        {
            int virtualNodeIndex = virtualNodes.BinarySearch(hash);
            if (virtualNodeIndex < 0)
            {
                virtualNodeIndex = ~virtualNodeIndex;
                if (virtualNodeIndex >= virtualNodes.Count)
                {
                    virtualNodeIndex = 0;
                }
                else if (virtualNodeIndex < 0) // Check if index is still negative after bitwise complement
                {
                    virtualNodeIndex = ~virtualNodeIndex; // Set it back to a positive index
                }
            }
            return virtualNodes[virtualNodeIndex];
        }

        private void UpdateFrequency(int hash, int valueToModifyFrequencyWith)
        {
            if (cacheNodes.ContainsKey(GetVirtualNodeIdFromHash(hash)))
            {
                //var (cachedValue, frequency) = cacheNodes[hash];
                var cachedData = cacheNodes[GetVirtualNodeIdFromHash(hash)];
                cachedData.frequency = cachedData.frequency + valueToModifyFrequencyWith;
                cacheNodes[GetVirtualNodeIdFromHash(hash)] = cachedData;
            }

            int currentFrequency = frequenciesAndAssociatedHashValues
                .Where(pair => pair.Value.Contains(hash))
                .Select(pair => pair.Key)
                .FirstOrDefault();

            if (currentFrequency != 0)
            {
                frequenciesAndAssociatedHashValues[currentFrequency].Remove(hash);

                if (frequenciesAndAssociatedHashValues[currentFrequency].Count() == 0)
                {
                    frequenciesAndAssociatedHashValues.Remove(currentFrequency);
                }

                int newFrequency = cacheNodes[GetVirtualNodeIdFromHash(hash)].frequency;

                if (!frequenciesAndAssociatedHashValues.ContainsKey(newFrequency))
                {
                    frequenciesAndAssociatedHashValues[newFrequency] = new HashSet<int>();
                }

                frequenciesAndAssociatedHashValues[newFrequency].Add(hash);
            }
            else
            {
                if (!cacheNodes.ContainsKey(GetVirtualNodeIdFromHash(hash)))
                    throw new Exception($"Virtual key '{GetVirtualNodeIdFromHash(hash)}' doesn't exist in the cache module, cannot update frequency!");
                int newFrequency = cacheNodes[GetVirtualNodeIdFromHash(hash)].frequency;
                if (!frequenciesAndAssociatedHashValues.ContainsKey(newFrequency))
                {
                    frequenciesAndAssociatedHashValues[newFrequency] = new HashSet<int>();
                }

                frequenciesAndAssociatedHashValues[newFrequency].Add(hash);
            }
        }


        public string PrintCacheContents()
        {
            string cacheContentsString = "";
            Console.WriteLine("Cache Contents:");

            foreach (var keyValuePair in cacheNodes)
            {
                cacheContentsString += $"Key: {keyValuePair.Key}, Value: {keyValuePair.Value.data}, Frequency: {keyValuePair.Value.frequency}\n";
            }
            return cacheContentsString;
        }

        public string PrintFrequentData(int nrOfHighestFrequencyElements)
        {
            string frequentCacheContents = $"Top {nrOfHighestFrequencyElements} Most Frequently Accessed Data:\n";

            int totalPrinted = 0;
            foreach (var keyValuePair in frequenciesAndAssociatedHashValues.Reverse())
            {
                foreach (var hash in keyValuePair.Value)
                {
                    frequentCacheContents += $"Hash: {hash}, Cached data: {cacheNodes[GetVirtualNodeIdFromHash(hash)].data} Frequency: {keyValuePair.Key}\n";
                    totalPrinted++;
                    if (totalPrinted >= nrOfHighestFrequencyElements)
                        return frequentCacheContents;
                }
            }
            return frequentCacheContents;

        }
    }
}
