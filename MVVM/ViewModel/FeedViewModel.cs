using System.Collections.ObjectModel;
using System.ComponentModel;
using UBB_SE_2024_Gaborment.MVVM.Model;
using UBB_SE_2024_Gaborment.Server;
using UBB_SE_2024_Gaborment.Server.LoggerUtils;

namespace UBB_SE_2024_Gaborment.MVVM.ViewModel
{
    public class FeedViewModel : BaseViewModel
    {
        public ObservableCollection<PostModel> Posts {  get; set; }
        public FeedViewModel()
        {
            Posts = new ObservableCollection<PostModel>();
            LoadPosts();
        }

        private void LoadPosts()
        {
            // Assuming you have a way to get the current user's ID and the desired date range
            string userId = "0";
            string idFeed = "-1";

            var applicationService = ApplicationService.Instance;
            var postsMock = applicationService.getFeedConfiguredPosts(userId, idFeed);

            List<FeedConfigurationDetails> feedConfigurationDetails = applicationService.getFeedConfigurationDetailsForUser(userId);


            // Convert PostMock to Post and populate the Posts collection
            Posts = new ObservableCollection<PostModel>(postsMock.Select(postMock => new PostModel
            {
                Username = postMock.GetUsernameOwner(),
                TextContent = postMock.GetText(),
                ID = postMock.GetID().ToString()
            }));
        }
    }
}
