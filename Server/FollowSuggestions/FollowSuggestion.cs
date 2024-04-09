using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Server.FollowSuggestions
{
    internal class FollowSuggestion:IComparable<FollowSuggestion>
    {
        public string userId;
        public string username;

        public int numberOfCommonFriends;
        public int numberOfCommonGroups; 
        public int numberOfCommonOrganizations; 
        public int numberOfCommonTags;
        public string location;

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

        public int CompareTo(FollowSuggestion? other)
        {
            throw new NotImplementedException();
        }
    }
}
