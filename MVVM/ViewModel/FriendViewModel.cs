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

namespace UBB_SE_2024_Gaborment.MVVM.ViewModel
{
    public class FriendViewModel : BaseViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public FriendViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            LoadUsers();
        }

        private void LoadUsers()
        {
           
        }
    }
}

