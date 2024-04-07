using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Server.FollowSuggestions
{
    internal class FollowSuggestion
    {
        private string userId { get; set; }
        private string username { get; set; }

        private int numberOfCommonFriends {  get; set; }
        private int numberOfCommonGroups { get; set; }
        private int numberOfCommonOrganizations { get; set; }
        private int numberOfCommonTags { get; set; }
        private string location { get; set; }

        public FollowSuggestion(string userId, string username, int numberOfCommonFriends, int numberOfCommonGroups, int numberOfCommonOrganizations, int numberOfCommonTags, string location)
        {
            this.userId = userId;
            this.username = username;
            this.numberOfCommonFriends = numberOfCommonFriends;
            this.numberOfCommonGroups = numberOfCommonGroups;
            this.numberOfCommonOrganizations = numberOfCommonOrganizations;
            this.numberOfCommonTags = numberOfCommonTags;
            this.location = location;
        }

    }
}
