
namespace UBB_SE_2024_Gaborment.Server.Mocks
{
    internal class UserMock
    {
        public string userId { get; set; }
        public string username { get; set; }
        public bool isPublic { get; set; }
        public List<string> tags { get; set; }
        public List<string> groups { get; set; }
        public List<string> locations { get; set; }
        public UserMock(string userId, string username, bool isPublic, List<string> tags, List<string> groups, List<string> locations)
        {
            this.userId = userId;
            this.username = username;
            this.isPublic = isPublic;
            this.tags = tags;
            this.groups = groups;
            this.locations = locations;
        }
    }
}
