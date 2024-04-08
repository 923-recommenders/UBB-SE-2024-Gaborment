﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;

namespace UBB_SE_2024_Gaborment.Server.Mocks
{
    internal class PostServiceMock
    {
        private PostRepositoryMock postsRepo;
        private FollowService followService;
        /*private UserServiceMock userService;*/

        public PostServiceMock(PostRepositoryMock postsRepo, FollowService followService)/*, UserServiceMock userService)*/
        {
            this.postsRepo = postsRepo;
            this.followService = followService;
            /*this.userService = userService;*/
        }

        public List<PostMock> searchVisiblePosts(String userId, DateTime startDate, DateTime endDate)
        {
            List<Follow> followers = this.followService.getFollowersOf(userId);
            List<PostMock> postsFromStartDateToEndDate = this.postsRepo.getPostsFromStartDateToEndDate(startDate, endDate);
            /*postsFromStartDateToEndDate.RemoveAll(post => post.GetOwner().userId == userId);*/
            List<PostMock> publicPostsFiltered = postsFromStartDateToEndDate.FindAll(post => post.GetOwner().isPublic);
            List<PostMock> privatePostsVisibleFiltered = postsFromStartDateToEndDate.FindAll(post => followers.Find(follower => follower.getReceiver() == post.GetOwner().userId) != null);
            return publicPostsFiltered.Union(privatePostsVisibleFiltered).ToList();
        }
    }
}
