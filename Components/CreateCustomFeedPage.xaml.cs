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
using UBB_SE_2024_Gaborment.Server.LoggerUtils;
using UBB_SE_2024_Gaborment.Session;

namespace UBB_SE_2024_Gaborment.Components
{
    public partial class CreateCustomFeedPage : UserControl
    {
        private List<string> usernames = new List<string>();
        private List<string> hashtags = new List<string>();
        private List<string> locations = new List<string>();
        private string feedName;

        public CreateCustomFeedPage()
        {
            InitializeComponent();
            txtUsernamesView.Clear();
            txtHashtagsView.Clear();
            txtLocationsView.Clear();
            txtFeedNameView.Clear();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = sender as TextBox;
                if (textBox != null)
                {
                    string input = textBox.Text.Trim();
                    if (!string.IsNullOrEmpty(input))
                    {
                        if (textBox == txtUsernames)
                        {
                            usernames.Add(input);
                            txtUsernamesView.Text = $"{txtUsernamesView.Text} {input}";
                        }
                        else if (textBox == txtHashtags)
                        {
                            hashtags.Add(input);
                            txtHashtagsView.Text = $"{txtHashtagsView.Text} {input}";
                        }
                        else if (textBox == txtLocations)
                        {
                            locations.Add(input);
                            txtLocationsView.Text = $"{txtLocationsView.Text} {input}";
                        }
                        else if (textBox == txtFeedName)
                        {
                            txtFeedNameView.Clear();
                            feedName = input;
                            txtFeedNameView.Text = $"{input} ";
                        }
                        textBox.Clear();
                    }
                }
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Configuration confirmed.");
            var session = ApplicationSession.Instance;
            var applicationService = ApplicationService.Instance;
            applicationService.addCustomFeed(session.CurrentUserId, feedName, hashtags, locations, usernames);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Configuration cancelled.");
            usernames.Clear();
            hashtags.Clear();
            locations.Clear();

        }
    }
}
