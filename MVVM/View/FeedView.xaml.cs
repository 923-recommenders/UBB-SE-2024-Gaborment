using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UBB_SE_2024_Gaborment.Server.LoggerUtils;

namespace UBB_SE_2024_Gaborment.MVVM.View
{
    /// <summary>
    /// Interaction logic for FeedView.xaml
    /// </summary>
    public partial class FeedView : UserControl
    {
        int pageIndex = 1;
        const int numberOfRecPerPage = 5;
        private enum PagingMode { Next = 1, Previous = 2 };
        List<Button> myList = new List<Button>();
        private int feedCount;
        List<FeedTemp> feedList= new List<FeedTemp>();
        List<string> posts = new List<string>();
        public FeedView()
        {
            InitializeComponent();
            feedCount = getAllFeedsCount();
            setFeeds();
            this.myList = GetButtonData();
            this.posts= feedList[0].posts;

            dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
        }

        int getAllFeedsCount() { return 7; }


        private DataGrid FindDataGrid(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is DataGrid dataGrid)
                {
                    return dataGrid;
                }
                var result = FindDataGrid(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        public class FeedTemp
        {
            public string id { get; set; }
            public string name { get; set; }

            public List<string> posts { get; set; }
        }

        private void setFeeds()
        {
            // Fetch the feed configuration details for the current user
            var applicationService = ApplicationService.Instance;
            var feedConfigurationDetails = applicationService.getFeedConfigurationDetailsForUser("userId"); // Replace "userId" with the actual user ID

            foreach (var feedDetail in feedConfigurationDetails)
            {
                FeedTemp feed = new FeedTemp();
                feed.id = feedDetail.feedId == -1 ? feedDetail.feedName : feedDetail.feedId.ToString();
                feed.name = feedDetail.feedName;
                feed.posts = new List<string> { $"{feedDetail.feedName} post" };
                feedList.Add(feed);
            }
        }



        private List<Button> GetButtonData()
        {
            List<Button> buttonList = new List<Button>();
            foreach(FeedTemp feed in feedList){
                Button button = new Button();
                if (feed.id != "-1")
                    button.Content = feed.name;
                if (feed.id == "HomeFeed")
                    button.Content = "Home Feed";
                if (feed.id == "TrendingFeed")
                    button.Content = "Trending Feed";
                if (feed.id == "FollowingFeed")
                    button.Content = "Following Feed";
                if (feed.id == "ControversialFeed")
                    button.Content = "Controversial Feed";
                button.Name = feed.id.ToString();
                button.Tag = feed.id;
                buttonList.Add(button);
            }
            return buttonList;
        }

        private void CarouselButtonClicked(object sender, RoutedEventArgs e)
        {
            // ATTENTION!!!!
            // Convert sender to Button type
            Button tempButton = sender as Button;

            // Get the string representation of the Button
            string buttonString = tempButton.ToString();

            // Find the index of the substring "Button" which marks the start of the name
            int startIndex = buttonString.IndexOf("Button: ");

            // Extract the substring starting from "Button " IMPORTANT WITH SPACE!!! to the end to get the name
            string nameString = buttonString.Substring(startIndex);

            // The name of the button is on pos 1
            string[] parts = nameString.Split(':');
            string tempButtonName = parts[1];
            parts = tempButtonName.Split(' ');
            string buttonId = $"{parts[1]}{parts[2]}";

            foreach (FeedTemp feed in feedList)
            {
                if (feed.id == buttonId)
                {
                    break;

                    //feedTextBlock.Text = feed.content;
                    //break;

                }
            }
        }



        private void Navigate(int mode)
        {
            switch (mode)
            {
                case (int)PagingMode.Next:
                    btnPrev.IsEnabled = true;
                    if (myList.Count >= (pageIndex * numberOfRecPerPage))
                    {
                        if (myList.Skip(pageIndex *
                        numberOfRecPerPage).Take(numberOfRecPerPage).Count() == 0)
                        {
                            dataGrid.ItemsSource = null;
                            dataGrid.ItemsSource = myList.Skip((pageIndex *
                            numberOfRecPerPage) - numberOfRecPerPage).Take(numberOfRecPerPage);
                        }
                        else
                        {
                            dataGrid.ItemsSource = null;
                            dataGrid.ItemsSource = myList.Skip(pageIndex *
                            numberOfRecPerPage).Take(numberOfRecPerPage);
                            pageIndex++;
                        }
                    }

                    else
                    {
                        btnNext.IsEnabled = false;
                    }

                    break;
                case (int)PagingMode.Previous:
                    btnNext.IsEnabled = true;
                    if (pageIndex > 1)
                    {
                        pageIndex -= 1;
                        dataGrid.ItemsSource = null;
                        if (pageIndex == 1)
                        {
                            dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
                        }
                        else
                        {
                            dataGrid.ItemsSource = myList.Skip
                            (pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage);
                        }
                    }
                    else
                    {
                        btnPrev.IsEnabled = false;
                    }
                    break;


            }
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Next);

        }

        private void btnPrev_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Previous);

        }

    }
}
