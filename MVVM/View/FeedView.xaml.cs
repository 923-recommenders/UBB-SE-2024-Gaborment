using System;
using System.Collections;
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
        public FeedView()
        {
            InitializeComponent();
            feedCount = getAllFeedsCount();
            this.myList = GetData();
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
            public string content { get; set; }
        }

        private List<FeedTemp> getFeeds()
        {
            List<FeedTemp> temp = new List<FeedTemp> ();
            for(int i=1;i<=7;i++) {
                FeedTemp feed = new FeedTemp();
                feed.id = "Feed"+i.ToString();
                feed.content = feed.id;
                temp.Add(feed); }
            return temp;
        }

        private List<Button> GetData()
        {
            List<Button> buttonList = new List<Button>();
            foreach(FeedTemp feed in getFeeds())
                {
                    Button button = new Button();
                    button.Content = "Button " + feed.id;
                    buttonList.Add(button);
                    button.Click += Button_Click;
                    button.Name = feed.id.ToString();
                }
            return buttonList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Cast the sender object to a Button to access its properties
            Button clickedButton = sender as Button;

            // Check if the clickedButton is not null and save its name in the variable
            if (clickedButton != null)
            {

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
