using UBB_SE_2024_Gaborment.Server.Relationships.Follow;

namespace UBB_SE_2024_Gaborment.Server.FollowSuggestions
{
    internal class FollowNetworkUtilities
    {
        List<string> GetNumberOfLevelsOfFollowFromNetworkStartingWithUser(string user, Dictionary<string, List<Follow>> followDictionary, int numberOfLevels)
        {
            Queue<string> userQueue = new Queue<string>();
            userQueue.Enqueue(user);

            Dictionary<string, int> visitedUsers = new Dictionary<string, int>();
            visitedUsers[user] = 0;

            List<string> layersOfFollows = new List<string>();
            int currentLevel = 0;

            while (userQueue.Count > 0)
            {
                string currentUser = userQueue.Dequeue();
                if (currentLevel > numberOfLevels)
                {
                    break;
                }
                layersOfFollows.Add(currentUser); 
                if (followDictionary.ContainsKey(currentUser))
                {
                    foreach (Follow follow in followDictionary[currentUser])
                    {
                        string follower = follow.getReceiver();
                        if (!visitedUsers.ContainsKey(follower))
                        {
                            userQueue.Enqueue(follower);
                            visitedUsers[follower] = visitedUsers[currentUser]+1;
                        }
                    }
                }
               
                currentLevel++;
            }

            return layersOfFollows;
        }
    }
}
