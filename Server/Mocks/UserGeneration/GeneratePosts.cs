﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace UBB_SE_2024_Gaborment.Server.Mocks.UserGeneration
{
    internal class GeneratePosts
    {
        public static List<PostMock> generateRandomPosts(int count, List<UserMock> users)
        {
            if(count < 0)
            {
                throw new ArgumentException("Invalid input -- please generate more than 0 elements.");
            }

            List<PostMock> posts = new List<PostMock>();
            List<string> predefinedTags = new List<string>{
                "instagood", "photooftheday", "beautiful", "happy", "cute", "love", "fashion", "tbt",
                "follow", "me", "selfie", "like4like", "picoftheday", "summer", "friends", "fun",
                "smile", "food", "style", "followme", "family", "nature", "instalike", "travel", "instadaily",
                "awesome", "awesomepic", "wanderlust", "adventure", "explore", "amazing", "bestoftheday",
                "life", "inspiration", "motivation", "fitness", "health", "wellness", "success", "goals",
                "entrepreneur", "business", "startup", "technology", "innovation", "creativity", "art",
                "music", "dance", "party", "celebrate", "weekend", "beach", "mountains", "cityscape",
                "architecture", "sunrise", "sunset", "reflection", "silhouette", "skyline", "landscape"
             };
            List<string> predefinedLocations = new List<string>
            {
                "London", "Paris", "Berlin", "Madrid", "Rome", "Amsterdam", "Vienna", "Prague", "Barcelona", "Lisbon",
                "Budapest", "Munich", "Milan", "Warsaw", "Brussels", "Stockholm", "Athens", "Copenhagen", "Dublin",
                "Zurich", "Oslo", "Helsinki", "Bucharest", "Reykjavik", "Edinburgh", "Sofia", "Tallinn", "Riga", "Vilnius",
                "Bratislava", "Ljubljana", "Luxembourg City", "Nicosia", "Valletta", "Andorra la Vella", "Vaduz", "Monaco",
                "San Marino", "Vatican City", "Dubrovnik", "Split", "Zagreb", "Sarajevo", "Belgrade", "Podgorica", "Tirana",
                "Skopje", "Kiev", "Minsk"
            };

            List<string> predefinedReactions = new List<string>
            {
                "like", "love", "dislike", "angry"
            };

            List<UserMock> usersCopy = new List<UserMock>(users);

            for (int i = 0; i < count; i++)
            {
                var faker = new Faker();
                Random random = new Random();

                int random_user_index = random.Next(0, usersCopy.Count - 1);
                UserMock owner = usersCopy[random.Next(0, random_user_index)];
                string text = faker.Lorem.Sentence();
                string location = predefinedLocations[random.Next(0, predefinedLocations.Count)];
                string mediaType = "image";

                List<string> hashtags = new List<string>();
                int rand_tag_count = faker.Random.Int(1, 10);
                foreach (string tag in predefinedTags)
                {
                    string random_tag = predefinedTags[random.Next(0, predefinedTags.Count)];
                }

                int numberOfComments = faker.Random.Int(0, 100);
                int numberOfViews = faker.Random.Int(0, 1000);
                DateTime postingDate = faker.Date.Past(1);

                int countAtLeastHalfPostsHaveMorePositiveReactions = count / 2;
                int positiveReactions = 0;
                int negativeReactions = 0;


                Dictionary<string, List<UserMock>> reactions = new Dictionary<string, List<UserMock>>();
                foreach (string reaction in predefinedReactions)
                {
                    List<UserMock> rand_users = new List<UserMock>();
                    int number_of_users = faker.Random.Int(0, 1000);
                    if(reaction == "like" || reaction == "love")
                    {
                        positiveReactions += number_of_users;
                    }
                    if(reaction == "dislike")
                    {
                        negativeReactions += number_of_users;
                    }
                    if(reaction == "angry")
                    {
                        while((positiveReactions < negativeReactions + number_of_users) && (countAtLeastHalfPostsHaveMorePositiveReactions > 0))
                        {
                            number_of_users = faker.Random.Int(0, 1000);
                            countAtLeastHalfPostsHaveMorePositiveReactions--;
                        }
                    }
                    for (int j = 0; j < number_of_users; j++)
                    {
                        rand_users.Add(new UserMock());
                    }
                    reactions.Add(reaction, rand_users);
                    
                }

                PostMock postMock = new PostMock(owner, text, location, mediaType, hashtags, numberOfViews,
                    numberOfComments, reactions, postingDate);
                posts.Add(postMock);
            }   

            return posts;
        }

        public static List<PostMock> GenerateGuaranteedControversialPosts(int count,
           int numberOfControversials, List<UserMock> users)
        {
            if(count < 0 || numberOfControversials < 0)
            {
                throw new ArgumentException("Invalid input -- please generate more than 0 elements.");
            }

            if(count == 0 && numberOfControversials == 0)
            {
                throw new Exception("Invalid input -- please generate at least one element. Both parameters are 0.");
            }


            List<PostMock> posts = generateRandomPosts(numberOfControversials, users);

            List<string> predefinedReactions = new List<string>
            {
                "like", "love", "dislike", "angry"
            };

             foreach (PostMock post in posts)
            {
                int positiveReactions = post.GetReactions()["like"] + post.GetReactions()["love"];
                int negativeReactions = post.GetReactions()["dislike"] + post.GetReactions()["angry"];
                int difference = Math.Abs(positiveReactions - negativeReactions);
          

                if (difference > 5)
                {
                    Random random = new Random();
                    int randomNormalizationDifference = random.Next(0, 5);
                    if(positiveReactions < negativeReactions)
                    {
                        List<UserMock> randomUsersAux = new List<UserMock>();
                        for (int i = 0; i < difference - randomNormalizationDifference; i++)
                        {
                            UserMock randomUserAux = new UserMock();
                            post.AddReaction("like", randomUserAux);

                        }
                    }
                    else
                    {
                        List<UserMock> randomUsersAux = new List<UserMock>();
                        for (int i = 0; i < difference - randomNormalizationDifference; i++)
                        {
                            UserMock randomUserAux = new UserMock();
                            post.AddReaction("dislike", randomUserAux);
                        }
                    }
                    
                }          
              
            }
            if(count > numberOfControversials)
            {
                List<PostMock> randomPosts = generateRandomPosts(count - numberOfControversials, users);
                posts.AddRange(randomPosts);
            } 
            return posts;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            List<UserMock> users = GenerateUsers.GenerateRandomUsers(10);
            //List<PostMock> posts = GeneratePosts.generateRandomPosts(10, users);
            List<PostMock> posts = GeneratePosts.GenerateGuaranteedControversialPosts(10, 5, users);
            foreach (PostMock post in posts)
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"Post ID: {post.GetID()}");
                Console.WriteLine($"Owner: {post.GetOwner().username}");
                Console.WriteLine($"Text: {post.GetText()}");
                Console.WriteLine($"Location: {post.GetLocation()}");
                Console.WriteLine($"Media Type: {post.GetMediaType()}");
                Console.WriteLine($"Number of Views: {post.GetViews()}");
                Console.WriteLine($"Number of comments: {post.GetNumberOfComments()}");
                Console.WriteLine("Reactions: ");
                foreach (string reaction in post.GetReactions().Keys)
                {
                    Console.WriteLine($"{reaction}: {post.GetReactions()[reaction]}");
                }
                if (post.GetReactions()["like"] + post.GetReactions()["love"] > post.GetReactions()["dislike"] + post.GetReactions()["angry"])
                {
                    Console.WriteLine("This post is more positive than negative.");
                }
                else
                {
                    Console.WriteLine("This post is more negative than positive.");
                }

            }
        }
    }
}
