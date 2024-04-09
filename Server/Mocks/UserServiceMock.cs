using System.Text;

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

        public List<UserMock> searchUsers(string searchToken)
        {
            searchToken = searchToken.Normalize(NormalizationForm.FormD).ToLowerInvariant().Replace(" ", ""); ;
            var allUsers = userRepository.GetAllUsers();

            var filteredUsers = allUsers.Where(user =>
            {
               
                string username = user.username.Normalize(NormalizationForm.FormD).ToLowerInvariant();
                string firstname = user.firstname.Normalize(NormalizationForm.FormD).ToLowerInvariant();
                string lastname = user.lastname.Normalize(NormalizationForm.FormD).ToLowerInvariant();

                return username.Contains(searchToken) ||
                       firstname.Contains(searchToken) ||
                       lastname.Contains(searchToken);
            }).ToList();

            return filteredUsers;
        }
    }
}
