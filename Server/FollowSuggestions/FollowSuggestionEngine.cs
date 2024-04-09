using UBB_SE_2024_Gaborment.Server.Mocks;
using UBB_SE_2024_Gaborment.Server.Relationships.Block;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;
using UBB_SE_2024_Gaborment.Server.Request;
using UBB_SE_2024_Gaborment.Server.Sorting;

namespace UBB_SE_2024_Gaborment.Server.FollowSuggestions
{
    internal class FollowSuggestionEngine
    {
        private readonly FollowService followService;
        private readonly BlockService blockService;
        private readonly RequestService requestService;
        private readonly UserServiceMock userService;
        public FollowSuggestionEngine(
            FollowService followService,
            BlockService blockService,
            RequestService requestService,
            UserServiceMock userService,
            Logger logger
        ) {
            this.followService = followService;
            this.blockService = blockService;
            this.requestService = requestService;
            this.userService = userService;
        }

        public List<FollowSuggestion> GetFollowSuggestionsForUser(string userId, AccountType accountType)
        {
            List<string> closeInNetworkUserIds = FollowNetworkUtilities.GetNumberOfLevelsOfFollowFromNetworkStartingWithUser(
                    userId,
                    followService.getAllFollowers(),
                    3
                );
            List<string> sanitizedUserIds = FollowNetworkUtilities.FilterOutRedundantSuggestedUserIds(
                closeInNetworkUserIds, followService.getFollowingUserIdsOf(userId),
                blockService.getBlockedUserIdsOf(userId), requestService.getRequestedUserIdsBy(userId));

            List<FollowSuggestion> followSuggestions = new List<FollowSuggestion>();

            foreach (var suggestedUserId in sanitizedUserIds)
            {
                UserMock suggestedUser = userService.GetUserById(suggestedUserId);

            
                int numberOfCommonFriends = CalculateCommonFriends(userId, suggestedUserId);
                int numberOfCommonGroups = CalculateCommonGroups(userId, suggestedUserId);
                int numberOfCommonOrganizations = CalculateCommonOrganizations(userId, suggestedUserId);
                int numberOfCommonTags = CalculateCommonTags(userId, suggestedUserId);

            
                FollowSuggestion followSuggestion = new FollowSuggestion(
                    suggestedUser.userId,
                    suggestedUser.username,
                    numberOfCommonFriends,
                    numberOfCommonGroups,
                    numberOfCommonOrganizations,
                    numberOfCommonTags,
                    suggestedUser.location
                );

                followSuggestions.Add(followSuggestion);
            }


            return followSuggestions;
        }

        private int CalculateCommon<T>(IEnumerable<T> userElements, IEnumerable<T> suggestedUserElements, Func<T, T, bool> comparisonFunction)
        {
            int commonCount = 0;
            foreach (var userElement in userElements)
            {
                foreach (var suggestedUserElement in suggestedUserElements)
                {
                    if (comparisonFunction(userElement, suggestedUserElement))
                    {
                        commonCount++;
                        break;
                    }
                }
            }
            return commonCount;
        }


        private List<FollowSuggestion> GetCustomSortedFollowSuggestions(List<FollowSuggestion> followSuggestions, AccountType accountType)
        {
            List<Func<FollowSuggestion, FollowSuggestion, int>> followSuggestionSortingCriterias = FollowSuggestionsSortingOrders.GetSortingOrderForAccountType(accountType);
            int numberOfSortedSuggestions = followSuggestions.Count();
            ISorter<FollowSuggestion> sorter = new MergeSort<FollowSuggestion>();
            for (int i = 1; i < followSuggestionSortingCriterias.Count; i++)
            {
                if (numberOfSortedSuggestions < 2)
                {
                    break;
                }
                List<FollowSuggestion> firstQuarter = followSuggestions.Take(numberOfSortedSuggestions).ToList();
                List<FollowSuggestion> otherQuarters = followSuggestions.Skip(numberOfSortedSuggestions).ToList();
                sorter.SortAscending(firstQuarter, followSuggestionSortingCriterias[i]);

                numberOfSortedSuggestions = numberOfSortedSuggestions / 4;
                followSuggestions = firstQuarter.Concat(otherQuarters).ToList();
            }
            return followSuggestions;
        }
        private int CalculateCommonFriends(string userId, string suggestedUserId)
        {
            var userFriends = followService.getFollowingUserIdsOf(userId);
            var suggestedUserFriends = followService.getFollowingUserIdsOf(suggestedUserId);
            Func<string, string, bool> areFriendsCommon = (friend1, friend2) => friend1 == friend2;
            return CalculateCommon(userFriends, suggestedUserFriends, areFriendsCommon);
        }


        private int CalculateCommonGroups(string userId, string suggestedUserId)
        {
            UserMock userMock = userService.GetUserById(userId);
            UserMock suggestedUserMock = userService.GetUserById(suggestedUserId);
            Func<string, string, bool> areGroupsCommon = (group1, group2) => group1== group2;
            return CalculateCommon(userMock.groups, suggestedUserMock.groups, areGroupsCommon);
        }
        private int CalculateCommonOrganizations(string userId, string suggestedUserId)
        {
            UserMock userMock = userService.GetUserById(userId);
            UserMock suggestedUserMock = userService.GetUserById(suggestedUserId);
            Func<string, string, bool> areOrganizationsCommon = (org1, org2) => org1 == org2;
            return CalculateCommon(userMock.organizations, suggestedUserMock.organizations, areOrganizationsCommon);
        }

        private int CalculateCommonTags(string userId, string suggestedUserId)
        {
            UserMock userMock = userService.GetUserById(userId);
            UserMock suggestedUserMock = userService.GetUserById(suggestedUserId);
            Func<string, string, bool> areTagsCommon = (tag1, tag2) => tag1 == tag2;
            return CalculateCommon(userMock.tags, suggestedUserMock.tags, areTagsCommon);
        }

    }
}
