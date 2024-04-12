using System.Windows;
using UBB_SE_2024_Gaborment.Server;
using UBB_SE_2024_Gaborment.Server.LoggerUtils;

namespace UBB_SE_2024_Gaborment
{
    public partial class App : Application
    {
        public App() {
            InitializeComponent();
            var appService = ApplicationService.Instance;
            var session = Session.ApplicationSession.Instance;
            FeedConfigurationDetails feedConfigurationDetails = new FeedConfigurationDetails("Trending Feed", Server.FeedConfigurations.FeedTypes.TrendingFeed,-1);
            session.FeedStartTime = DateTime.Now.AddYears(-1);
            session.FeedEndTime = DateTime.Now;
            session.CurrentFeedConfiguration = feedConfigurationDetails;
        }

    }

}
