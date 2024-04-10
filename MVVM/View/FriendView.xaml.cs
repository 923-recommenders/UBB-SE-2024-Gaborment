using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UBB_SE_2024_Gaborment.MVVM.ViewModel;
using UBB_SE_2024_Gaborment.Server.FollowSuggestions;
using UBB_SE_2024_Gaborment.Server.LoggerUtils;
using UBB_SE_2024_Gaborment.Server.Mocks;

namespace UBB_SE_2024_Gaborment.MVVM.View
{
    public partial class FriendView : UserControl
    {
        private FriendViewModel _viewModel;

        public FriendView()
        {
            InitializeComponent();
            _viewModel = new FriendViewModel();
            DataContext = _viewModel;
            RequestsControl.Visibility = Visibility.Collapsed;
            ItemsControl.Visibility = Visibility.Visible;
            _viewModel.LoadFollowSuggestions();

        }

        private void FriendsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.CurrentState = "Friends";
            RequestsControl.Visibility = Visibility.Collapsed;
            ItemsControl.Visibility = Visibility.Visible;
        }

        private void FollowingButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.CurrentState = "Following";
            RequestsControl.Visibility = Visibility.Collapsed;
            ItemsControl.Visibility = Visibility.Visible;
        }

        private void FollowersButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.CurrentState = "Followers";
            RequestsControl.Visibility = Visibility.Collapsed;
            ItemsControl.Visibility = Visibility.Visible;
        }

        private void BlockedButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.CurrentState = "Blocked";
            RequestsControl.Visibility = Visibility.Collapsed;
            ItemsControl.Visibility = Visibility.Visible;
        }

        private void RequestsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.CurrentState = "Requests";
            ItemsControl.Visibility = Visibility.Collapsed;
            RequestsControl.Visibility = Visibility.Visible;

        }
    }
}
