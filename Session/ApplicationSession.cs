using UBB_SE_2024_Gaborment.Server;

namespace UBB_SE_2024_Gaborment.Session
{
    internal class ApplicationSession
    {
        private static ApplicationSession _instance;
        private string _currentUserId = "1";
        private FeedConfigurationDetails _currentFeedConfiguration;
        private List<FeedConfigurationDetails> _allFeedConfigurations;
        private DateTime _feedStartTime;
        private DateTime _feedEndTime;

        private ApplicationSession()
        {
        }

        public static ApplicationSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApplicationSession();
                }
                return _instance;
            }
        }

        public string CurrentUserId
        {
            get { return _currentUserId; }
            set { _currentUserId = value; }
        }

        public FeedConfigurationDetails CurrentFeedConfiguration
        {
            get { return _currentFeedConfiguration; }
            set { _currentFeedConfiguration = value; }
        }

        public List<FeedConfigurationDetails> AllFeedConfigurations
        {
            get { return _allFeedConfigurations; }
            set { _allFeedConfigurations = value; }
        }

        public DateTime FeedStartTime
        {
            get { return _feedStartTime; }
            set { _feedStartTime = value; }
        }

        public DateTime FeedEndTime
        {
            get { return _feedEndTime; }
            set { _feedEndTime = value; }
        }
    }
}
