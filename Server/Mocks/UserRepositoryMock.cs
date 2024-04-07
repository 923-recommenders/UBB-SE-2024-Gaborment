
namespace UBB_SE_2024_Gaborment.Server.Mocks
{
    internal class UserRepositoryMock
    {
        private List<UserMock> users = new List<UserMock>
        {
            new UserMock("1", "User1", true, new List<string> { "Tag1" }, new List<string> { "Group1" }, new List<string> { "Location1" }, "l1"),
            new UserMock("2", "User2", false, new List<string> { "Tag2" }, new List<string> { "Group2" }, new List<string> { "Location2" }, "l1"),
            new UserMock("3", "User3", true, new List<string> { "Tag3" }, new List<string> { "Group3" }, new List<string> { "Location3" }, "l1"),
            new UserMock("4", "User4", false, new List<string> { "Tag4" }, new List<string> { "Group4" }, new List<string> { "Location4" }, "l1"),
            new UserMock("5", "User5", true, new List<string> { "Tag5" }, new List<string> { "Group5" }, new List<string> { "Location5" }, "l1")
        };

        public List<UserMock> GetAllUsers()
        {
            return users;
        }

        public UserMock GetUserById(string id)
        {
            return users.FirstOrDefault(user=>user.userId == id);
        }
    }
}
