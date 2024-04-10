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

        public void LoadUsers()
        {
            ApplicationService service = ApplicationService.Instance;
            List<UserMock> users = new List<UserMock>();
            
            users = service.getPeopleUserIsFollowing("1");
            Users = new ObservableCollection<UserMock>(users);
        }
    }
}

