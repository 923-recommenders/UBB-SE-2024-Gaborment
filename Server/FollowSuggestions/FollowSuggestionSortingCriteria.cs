using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Server.FollowSuggestions
{
    internal class FollowSuggestionSortingCriteria
    {
        public static Func<FollowSuggestion, FollowSuggestion, int> commonFriendsComparisonFunction = (FollowSuggestion thisFollowSuggestion, FollowSuggestion  anotherFollowSuggestion) => thisFollowSuggestion.numberOfCommonFriends - anotherFollowSuggestion.numberOfCommonFriends;
        public static Func<FollowSuggestion, FollowSuggestion, int> commonGroupsComparisonFunction = (FollowSuggestion thisFollowSuggestion, FollowSuggestion anotherFollowSuggestion) => thisFollowSuggestion.numberOfCommonGroups - anotherFollowSuggestion.numberOfCommonGroups;
        public static Func<FollowSuggestion, FollowSuggestion, int> commonOrganizationsComparisonFunction = (FollowSuggestion thisFollowSuggestion, FollowSuggestion anotherFollowSuggestion) => thisFollowSuggestion.numberOfCommonOrganizations- anotherFollowSuggestion.numberOfCommonOrganizations;
        public static Func<FollowSuggestion, FollowSuggestion, int> commonTagsComparisonFunction = (FollowSuggestion thisFollowSuggestion, FollowSuggestion anotherFollowSuggestion) => thisFollowSuggestion.numberOfCommonTags - anotherFollowSuggestion.numberOfCommonTags;

    }
}
