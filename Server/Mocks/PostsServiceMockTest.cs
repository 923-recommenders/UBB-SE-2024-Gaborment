using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;
using UBB_SE_2024_Gaborment.Server.Relationships.Block;

namespace UBB_SE_2024_Gaborment.Server.Mocks
{
    internal class PostsServiceMockTest
    {
        public void TestPostsServiceMock()
        {
            UserMock user1 = new UserMock("1", "User1", true, new List<string> { "Tag1" }, new List<string> { "Group1" }, new List<string> { "Organization1" }, "Location1" );
            UserMock user2 = new UserMock("2", "User2", false, new List<string> { "Tag2" }, new List<string> { "Group2" }, new List<string> { "Organization2" }, "Location2" );
            UserMock user3 = new UserMock("3", "User3", true, new List<string> { "Tag3" }, new List<string> { "Group3" }, new List<string> { "Organization3" }, "Location3" );
            UserMock user4 = new UserMock("4", "User4", false, new List<string> { "Tag4" }, new List<string> { "Group4" }, new List<string> { "Organization4" }, "Location4" );
            UserMock user5 = new UserMock("5", "User5", true, new List<string> { "Tag5" }, new List<string> { "Group5" }, new List<string> { "Organization5" }, "Location5" );
            UserMock user6 = new UserMock("6", "User6", false, new List<string> { "Tag6" }, new List<string> { "Group6" }, new List<string> { "Organization6" }, "Location6" );

            PostMock post1 = new PostMock();
            post1.SetOwner(user1);
            PostMock post2 = new PostMock();
            post2.SetOwner(user2);
            PostMock post3 = new PostMock();
            post3.SetOwner(user3);
            PostMock post4 = new PostMock();
            post4.SetOwner(user4);
            post4.PostingDate = new DateTime(2024, 04, 01, 10, 0, 0);
            PostMock post5 = new PostMock();
            post5.SetOwner(user5);
            PostMock post6 = new PostMock();
            post6.SetOwner(user6);
            PostMock post7 = new PostMock();
            post7.SetOwner(user2);

            List<PostMock> posts = new List<PostMock>();
            posts.Add(post1);
            posts.Add(post2);
            posts.Add(post3);
            posts.Add(post4);
            posts.Add(post5);
            posts.Add(post6);
            posts.Add(post7);

            PostRepositoryMock postRepositoryMock = new PostRepositoryMock(posts);


            FollowService followService = new FollowService(new BlockRepository(), new FollowRepository());
            followService.createFollow("1", "2");
            followService.createFollow("1", "3");
            followService.createFollow("1", "4");


            PostServiceMock postServiceMock = new PostServiceMock(postRepositoryMock, followService);
            List<PostMock> expectedResult = new List<PostMock>();
            expectedResult.Add(post1);
            expectedResult.Add(post2);
            expectedResult.Add(post3);
            expectedResult.Add(post5);
            expectedResult.Add(post7);
            List<PostMock> returnedResult = postServiceMock.searchVisiblePosts("1", new DateTime(2024, 04, 05, 8, 0, 0), DateTime.Now);
            
            bool isOk = true;
            foreach(PostMock expectedPost in expectedResult)
            {
                if (!returnedResult.Contains(expectedPost))
                    isOk = false;
            }
            if (isOk && returnedResult.Count == expectedResult.Count)
                Console.WriteLine("PostsServiceMock is ok");
            else
                Console.WriteLine("PostsServiceMock is NOT ok");
        }

    }
}
