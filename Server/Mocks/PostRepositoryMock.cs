using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Server.Mocks
{
    internal class PostRepositoryMock
    {
        private List<PostMock> postsList;

        public PostRepositoryMock()
        {
            this.postsList = new List<PostMock>();
        }

        public PostRepositoryMock(List<PostMock> postsList)
        {
            this.postsList = postsList;
        }

        public List<PostMock> getPostsFromStartDateToEndDate(DateTime startDate, DateTime endDate)
        {
            return this.postsList.FindAll(post => (DateTime.Compare(startDate, post.PostingDate) <= 0 && DateTime.Compare(post.PostingDate, endDate) <= 0));
        }
    }
}
