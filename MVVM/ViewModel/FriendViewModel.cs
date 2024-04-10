using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Gaborment.MVVM;
using UBB_SE_2024_Gaborment.MVVM.Model;
using UBB_SE_2024_Gaborment.Server.LoggerUtils;
using UBB_SE_2024_Gaborment.Server;
using UBB_SE_2024_Gaborment.Server.Mocks;
using UBB_SE_2024_Gaborment.Server.Relationships.Follow;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Input;
using UBB_SE_2024_Gaborment.Server.FollowSuggestions;
using System.Windows;


namespace UBB_SE_2024_Gaborment.MVVM.ViewModel
{
    internal class FriendViewModel : BaseViewModel
    {
        public ObservableCollection<UserMock> Users { get; set; }
        public ObservableCollection<UserMock> UserSuggestionTrue { get; set; }

        private string _currentState;
        public string CurrentState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
                LoadUsers();
            }
        }

        public FriendViewModel()
        {
            UserSuggestionTrue = new ObservableCollection<UserMock>();
            Users = new ObservableCollection<UserMock>();
            CurrentState = "Friends"; // Set the initial state
        }

        public void LoadUsers()
        {
            ApplicationService service = ApplicationService.Instance;
            List<UserMock> users = new List<UserMock>();

            switch (CurrentState)
            {
                case "Friends":
                    IEnumerable<UserMock> commonUsers;
                    commonUsers = service.getPeopleUserIsFollowing("1").AsQueryable().Intersect(service.getPeopleUserIsBeingFollowedBy("1"));
                    users = commonUsers.ToList();
                    break;
                case "Following":
                    users = service.getPeopleUserIsFollowing("1");
                    break;
                case "Followers":
                    users = service.getPeopleUserIsBeingFollowedBy("1");
                    break;
                case "Blocked":
                    users = service.getPeopleUserBlocked("1");
                    break;
                case "Requests":
                    users = service.getRequestsUserReceived("1");
                    break;
                default:
                    break;
            }

            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }

        }
        public void LoadFollowSuggestions()
        {
            ApplicationService service = ApplicationService.Instance;
            List<FollowSuggestion> followSuggestions = service.GetFollowSuggestionsForUser();

            UserSuggestionTrue.Clear();
            foreach (var suggestion in followSuggestions.Take(3)) 
            {
                UserSuggestionTrue.Add(new UserMock { userId = suggestion.userId, username = suggestion.username});
            }
        }
    }

}