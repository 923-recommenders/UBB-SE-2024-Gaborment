using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UBB_SE_2024_Gaborment.Components;
using UBB_SE_2024_Gaborment.MVVM.ViewModel;
using UBB_SE_2024_Gaborment.Server;
using UBB_SE_2024_Gaborment.Server.LoggerUtils;
using UBB_SE_2024_Gaborment.Session;

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
        public FeedView()
        {
            InitializeComponent();
            this.myList = GetButtonData();
            dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
        }

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


        private List<Button> GetButtonData()
        {
            List<Button> buttonList = new List<Button>();
            var applicationService = ApplicationService.Instance;
            var feedConfigurationDetails = applicationService.getFeedConfigurationDetailsForUser(ApplicationSession.Instance.CurrentUserId);
            foreach(FeedConfigurationDetails feedConfig in feedConfigurationDetails)
            {
                Button button = new Button();
                if (feedConfig.feedId != -1)
                {
                    button.Content = feedConfig.feedName;
                    /*button.Name = feedConfig.feedId.ToString();*/
                    button.Name = feedConfig.feedName;
                }
                else
                {
                    if (feedConfig.feedName == "HomeFeed")
                        button.Content = "Home Feed";
                    if (feedConfig.feedName == "TrendingFeed")
                        button.Content = "Trending Feed";
                    if (feedConfig.feedName == "FollowingFeed")
                        button.Content = "Following Feed";
                    if (feedConfig.feedName == "ControversialFeed")
                        button.Content = "Controversial Feed";
                    button.Name = feedConfig.feedName;
                }
                buttonList.Add(button);
            } 
            return buttonList;
        }
        private void ConfigureFeedButton_Click(object sender, RoutedEventArgs e)
        {
            if (createCustomFeedPage.Visibility == Visibility.Collapsed)
            {
                createCustomFeedPage.Visibility = Visibility.Visible;
                configureFeedButton.Content = "Cancel";
                // Optionally, hide other parts of the UI or disable certain controls
            }
            else
            {
                MessageBox.Show("Configuration canceled.");
                createCustomFeedPage.Visibility = Visibility.Collapsed;
                configureFeedButton.Content = "Add Feed";
                // Optionally, show other parts of the UI or enable certain controls
            }
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
            string buttonId;
            if (parts.Length == 3)
                buttonId = $"{parts[1]}{parts[2]}";
            else
            {
                string temp = parts[1];
                startIndex = temp.IndexOf("d");
                buttonId = temp.Substring(startIndex+1);
            }

            var applicationService = ApplicationService.Instance;
            var feedConfigurationDetails = applicationService.getFeedConfigurationDetailsForUser(ApplicationSession.Instance.CurrentUserId);
            FeedConfigurationDetails selectedFeedConfiguration = feedConfigurationDetails.FirstOrDefault(feed => feed.feedId.ToString() == buttonId || feed.feedName == buttonId);

            if (selectedFeedConfiguration != null)
            {
                ApplicationSession.Instance.CurrentFeedConfiguration = selectedFeedConfiguration;
                var feedViewModel = this.DataContext as FeedViewModel;
                if (feedViewModel != null)
                {
                    feedViewModel.ReloadPosts();
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

        private void removeFeedButton_Click(object sender, RoutedEventArgs e)
        {
            var applicationSession = ApplicationSession.Instance;
            var applicationService = ApplicationService.Instance;
            if(applicationSession.CurrentFeedConfiguration.feedType == Server.FeedConfigurations.FeedTypes.CustomFeed)
            {
                applicationService.deleteCustomFeed(applicationSession.CurrentUserId, applicationSession.CurrentFeedConfiguration.feedId);

                this.myList = GetButtonData();
                dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
            }
        }
    }
}
