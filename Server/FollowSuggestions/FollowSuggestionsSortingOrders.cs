using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Gaborment.Server.Mocks;

namespace UBB_SE_2024_Gaborment.Server.FollowSuggestions
{
    internal class FollowSuggestionsSortingOrders
    {
        private static List<Func<FollowSuggestion, FollowSuggestion, int>> CASUAL_SORTING_ORDER = new List<Func<FollowSuggestion, FollowSuggestion, int>>{
            FollowSuggestionSortingCriteria.commonFriendsComparisonFunction,
            FollowSuggestionSortingCriteria.commonTagsComparisonFunction,
            FollowSuggestionSortingCriteria.commonGroupsComparisonFunction,
            FollowSuggestionSortingCriteria.commonOrganizationsComparisonFunction
        };

        private static List<Func<FollowSuggestion, FollowSuggestion, int>> PROFESSIONAL_SORTING_ORDER = new List<Func<FollowSuggestion, FollowSuggestion, int>>{
            FollowSuggestionSortingCriteria.commonOrganizationsComparisonFunction,
            FollowSuggestionSortingCriteria.commonTagsComparisonFunction,            
            FollowSuggestionSortingCriteria.commonGroupsComparisonFunction
        };

        public static List<Func<FollowSuggestion, FollowSuggestion, int>> GetSortingOrderForAccountType(AccountType accountType)
        {
            if (accountType == AccountType.CASUAL_ACCOUNT)
            {
                return CASUAL_SORTING_ORDER;
            } else
            {
                return PROFESSIONAL_SORTING_ORDER;
            }
        }
    }
}
