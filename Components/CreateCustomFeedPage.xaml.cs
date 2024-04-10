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
            MessageBox.Show("Configuration confirmed.");
            var session = ApplicationSession.Instance;
            var applicationService = ApplicationService.Instance;
        }
    }
}
