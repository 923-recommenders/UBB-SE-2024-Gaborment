using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBB_SE_2024_Gaborment.Server.Mocks;

namespace UBB_SE_2024_Gaborment.Server.FeedConfigurations
{
    internal class ControversialFeed:FeedConfiguration
    {
        int MinimumReactionCount { get; set; }
        int MinimumCommentCount { get; set; }

        public ControversialFeed(int minimumReactionCount, int minimumCommentCount)
        {
            this.MinimumReactionCount = minimumReactionCount;
            this.MinimumCommentCount = minimumCommentCount;
        }

        public ControversialFeed()
        {
            this.MinimumCommentCount = new int();
            this.MinimumReactionCount = new int();
        }

        public override int GetPostScore(PostMock post)
        {
            int numberOfReactions = post.GetReactions().Values.Sum();
            if (numberOfReactions < MinimumReactionCount ||
                post.GetNumberOfComments() < MinimumCommentCount)
            {
                return 0;
            }

            int numberOfLikes = post.GetReactions().TryGetValue("like", out int likes) ? likes : 0;
            int numberOfLoves = post.GetReactions().TryGetValue("love", out int loves) ? loves : 0;
            int numberOfDislikes = post.GetReactions().TryGetValue("dislike", out int dislikes) ? dislikes : 0;
            int numberOfAngrys = post.GetReactions().TryGetValue("angry", out int angrys) ? angrys : 0;

            int positive = numberOfLikes + numberOfLoves;
            int negative = numberOfDislikes + numberOfAngrys;

            return (numberOfReactions/ Math.Max(Math.Abs(positive - negative), 1));
        }

    }

}
