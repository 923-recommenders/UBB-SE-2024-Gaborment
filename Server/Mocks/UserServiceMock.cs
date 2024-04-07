namespace UBB_SE_2024_Gaborment.Server.Mocks
{
    internal class UserServiceMock
    {
        private UserRepositoryMock userRepository;

        public UserServiceMock()
        {
            userRepository = new UserRepositoryMock();
        }

        public List<UserMock> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }        

        public UserMock GetUserById(string userId)
        {
            return userRepository.GetUserById(userId);
        }
    }
}
