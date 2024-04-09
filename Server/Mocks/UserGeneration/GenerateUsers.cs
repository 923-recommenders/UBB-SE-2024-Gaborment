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

            List<string> predefinedOrganizations = new List<string>
            {
                "United Nations",
                "Doctors Without Borders",
                "Greenpeace",
                "World Wildlife Fund",
                "Amnesty International",
                "Red Cross",
                "Oxfam"
            };

            List<string> predefinedGroups = new List<string>
            {
                "Study Group Alpha",
                "Coding Club",
                "Chess Society",
                "Book Club",
                "Environmental Awareness Group",
                "Community Outreach Team",
                "Photography Club",
                "Dance Ensemble",
                "Debating Society",
                "Music Band"
            };



            for (int i = 0; i < numberOfUsers; i++)
            {
                var faker = new Faker();
                Random random = new Random();

                string userId = i.ToString();
                string username = faker.Person.UserName;
                string firstname = faker.Person.FirstName;
                string lastname = faker.Person.LastName;
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
                HashSet<string> selectedGroups = new HashSet<string>();

                int rand_number_of_groups = faker.Random.Int(1, predefinedGroups.Count);

                while (groups.Count < rand_number_of_groups)
                {
                    int randomIndex = faker.Random.Int(0, predefinedGroups.Count - 1);
                    string selectedGroup = predefinedGroups[randomIndex];

                    // Check if the group has already been selected
                    if (!selectedGroups.Contains(selectedGroup))
                    {
                        groups.Add(selectedGroup);
                        selectedGroups.Add(selectedGroup);
                    }
                }

                List<string> organizations = new List<string>();
                HashSet<string> selectedOrganizations = new HashSet<string>();

                int rand_number_of_organizations = faker.Random.Int(1, predefinedOrganizations.Count);

                while (organizations.Count < rand_number_of_organizations)
                {
                    int randomIndex = faker.Random.Int(0, predefinedOrganizations.Count - 1);
                    string selectedOrganization = predefinedOrganizations[randomIndex];

                    // Check if the organization has already been selected
                    if (!selectedOrganizations.Contains(selectedOrganization))
                    {
                        organizations.Add(selectedOrganization);
                        selectedOrganizations.Add(selectedOrganization);
                    }
                }

                var user = new UserMock(userId, username, isPublic, tags, groups, organizations, location, firstname, lastname);
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
