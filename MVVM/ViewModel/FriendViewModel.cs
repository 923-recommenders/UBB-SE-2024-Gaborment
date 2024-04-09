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

namespace UBB_SE_2024_Gaborment.MVVM.ViewModel
{
    internal class FriendViewModel : BaseViewModel
    {
        public ObservableCollection<UserMock> Users { get; set; }

        public FriendViewModel()
        {
            Users = new ObservableCollection<UserMock>();
            LoadUsers();
        }

        private void LoadUsers()
        {
            ApplicationService service = ApplicationService.Instance;
            List<UserMock> users = new List<UserMock>();
            for (int i = 0; i < 10; i++)
            {
                UserMock user = new UserMock(
                    userId: $"user_{i}",
                    username: $"username_{i}",
                    isPublic: true,
                    tags: new List<string> { $"tag_{i}" },
                    groups: new List<string> { $"group_{i}" },
                    organizations: new List<string> { $"organization_{i}" },
                    location: $"Location_{i}",
                    firstname: $"Firstname_{i}",
                    lastname: $"Lastname_{i}"
                );
                users.Add(user);
                
            }
            Users = new ObservableCollection<UserMock>(users);
        }
    }
}

