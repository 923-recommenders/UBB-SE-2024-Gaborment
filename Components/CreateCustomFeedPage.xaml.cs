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

namespace UBB_SE_2024_Gaborment.Components
{
    /// <summary>
    /// Interaction logic for CreateCustomFeedPage.xaml
    /// </summary>
    public partial class CreateCustomFeedPage : UserControl
    {
        public CreateCustomFeedPage()
        {
            InitializeComponent();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Implement your logic for confirming the configuration
            // For example, you might want to save the input values and close the page
            MessageBox.Show("Configuration confirmed.");
        }
    }
}
