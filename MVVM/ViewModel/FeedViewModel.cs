using System.Collections.ObjectModel;
using System.ComponentModel;
using UBB_SE_2024_Gaborment.MVVM.Model;
using UBB_SE_2024_Gaborment.Server.LoggerUtils;
using UBB_SE_2024_Gaborment.Server;
using UBB_SE_2024_Gaborment.Session;
using UBB_SE_2024_Gaborment.Server.Mocks;
using UBB_SE_2024_Gaborment.Components;

namespace UBB_SE_2024_Gaborment.MVVM.ViewModel
{
    internal class FeedViewModel : BaseViewModel
    {
        public ObservableCollection<PostMock> Posts { get; set; }

        public FeedViewModel()
        {
            Posts = new ObservableCollection<PostMock>();
            LoadPosts();
        }

        private void LoadPosts()
        {
            var applicationService = ApplicationService.Instance;
            var postsMock = applicationService.getFeedConfiguredPosts();
            Posts = new ObservableCollection<PostMock>(postsMock);
        }

        public void ReloadPosts()
        {
            Posts.Clear();
            LoadPosts();
            OnPropertyChanged(nameof(Posts));
        }
    }
}
