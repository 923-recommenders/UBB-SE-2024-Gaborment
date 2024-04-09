using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus; 

namespace UBB_SE_2024_Gaborment.Server.Mocks.UserGeneration
{
    internal class GenerateUsers
    {
        public static List<UserMock> GenerateRandomUsers(int numberOfUsers)
        {
            List<UserMock> users = new List<UserMock>();
            List<string> predefinedTags = new List<string>{
                "instagood", "photooftheday", "beautiful", "happy", "cute", "love", "fashion", "tbt",
                "follow", "me", "selfie", "like4like", "picoftheday", "summer", "friends", "fun",
                "smile", "food", "style", "followme", "family", "nature", "instalike", "travel", "instadaily"
            };
            List<string> predefinedLocations = new List<string>
            {
                "London", "Paris", "Berlin", "Madrid", "Rome", "Amsterdam", "Vienna", "Prague", "Barcelona", "Lisbon",
                "Budapest", "Munich", "Milan", "Warsaw", "Brussels", "Stockholm", "Athens", "Copenhagen", "Dublin",
                "Zurich", "Oslo", "Helsinki", "Bucharest", "Reykjavik", "Edinburgh", "Sofia", "Tallinn", "Riga", "Vilnius",
                "Bratislava"
            };



            for (int i = 0; i < numberOfUsers; i++)
            {
                var faker = new Faker();
                Random random = new Random();

                string userId = i.ToString();
                string username = faker.Person.UserName;
                bool isPublic = faker.Random.Bool();

                List<string> tags = new List<string>();
                int rand_tag_count = faker.Random.Int(1, 20);
               
                for (int j = 0; j < rand_tag_count; j++)
                {
                    int randomIndex = random.Next(0, predefinedTags.Count);
                    tags.Add(predefinedTags[randomIndex]); 
                }

                string location = predefinedLocations[random.Next(0, predefinedLocations.Count)];

                List<string> groups = new List<string>();
                string[] rand_groups = faker.Random.WordsArray(2);
                foreach (string group in rand_groups)
                {
                    groups.Add(group);
                }

                List<string> organizations = new List<string>();
                string[] rand_organizations = faker.Random.WordsArray(2);
                foreach (string organization in rand_organizations)
                {
                    organizations.Add(organization);
                }
                
                var user = new UserMock(userId, username, isPublic, tags, groups, organizations, location);
                users.Add(user);
            }
            return users;
        }
    }
    
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        List<UserMock> users = GenerateUsers.GenerateRandomUsers(10);
    //        foreach (UserMock user in users)
    //        {
    //            Console.WriteLine($"User ID: {user.userId}");
    //            Console.WriteLine($"Username: {user.username}");
    //            Console.WriteLine($"Location: {user.location}");
    //            Console.WriteLine($"Is Public: {user.isPublic}");
    //            Console.WriteLine("Tags: ");
    //            foreach (string tag in user.tags)
    //            {
    //                Console.WriteLine(tag);
    //            }
    //            Console.WriteLine("Groups: ");
    //            foreach (string group in user.groups)
    //            {
    //                Console.WriteLine(group);
    //            }
    //            Console.WriteLine("Organizations: ");
    //            foreach (string organization in user.organizations)
    //            {
    //                Console.WriteLine(organization);
    //            }
    //            Console.WriteLine();
    //        }
    //    }
    //}
}
