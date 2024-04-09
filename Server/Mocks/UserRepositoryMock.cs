
using UBB_SE_2024_Gaborment.Server.Mocks.UserGeneration;
namespace UBB_SE_2024_Gaborment.Server.Mocks
{
    internal class UserRepositoryMock
    {
        private List<UserMock> users;

        public UserRepositoryMock() {
            users = UserGeneration.GenerateUsers.GenerateRandomUsers(70);
        }

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
