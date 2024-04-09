using Microsoft.Data.SqlClient;
using UBB_SE_2024_Gaborment.Database;


namespace UBB_SE_2024_Gaborment.Server.Relationships.Follow
{
    internal class FollowRepository
    {
        private readonly ApplicationDatabaseContext _databaseHelper;

        public FollowRepository(ApplicationDatabaseContext databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public void AddFollow(Follow follow)
        {
            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Follows (Sender, Receiver, IsCloseFriend, ExpirationTimeStamp, Description) VALUES (@Sender, @Receiver, @IsCloseFriend, @ExpirationTimeStamp, @Description)", connection))
                {
                    command.Parameters.AddWithValue("@Sender", follow.getSender());
                    command.Parameters.AddWithValue("@Receiver", follow.getReceiver());
                    command.Parameters.AddWithValue("@IsCloseFriend", follow.getCloseFriendStatus());
                    command.Parameters.AddWithValue("@ExpirationTimeStamp", follow.getExpirationTimeStamp());
                    command.Parameters.AddWithValue("@Description", follow.getDescription());
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveFollow(string sender, string receiver)
        {
            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Follows WHERE Sender = @Sender AND Receiver = @Receiver", connection))
                {
                    command.Parameters.AddWithValue("@Sender", sender);
                    command.Parameters.AddWithValue("@Receiver", receiver);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Follow> GetFollowersOf(string sender)
        {
            var follows = new List<Follow>();
            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT Receiver, IsCloseFriend, ExpirationTimeStamp, Description FROM Follows WHERE Sender = @Sender", connection))
                {
                    command.Parameters.AddWithValue("@Sender", sender);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            follows.Add(new Follow(
                                sender,
                                reader.GetString(0),
                                reader.GetBoolean(1),
                                reader.GetDateTime(2),
                                reader.GetString(3)
                            ));
                        }
                    }
                }
            }
            return follows;
        }

        public List<Follow> GetFollowingOf(string receiver)
        {
            var follows = new List<Follow>();
            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT Sender, IsCloseFriend, ExpirationTimeStamp, Description FROM Follows WHERE Receiver = @Receiver", connection))
                {
                    command.Parameters.AddWithValue("@Receiver", receiver);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            follows.Add(new Follow
                            (
                                reader.GetString(0),
                                receiver,
                                reader.GetBoolean(1),
                                reader.GetDateTime(2),
                                reader.GetString(3)
                            ));
                        }
                    }
                }
            }
            return follows;
        }


        public List<Follow> GetFollowers()
        {
            var follows = new List<Follow>();
            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT Sender, Receiver, IsCloseFriend, ExpirationTimeStamp, Description FROM Follows", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            follows.Add(new Follow
                            (
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetBoolean(2),
                                reader.GetDateTime(3),
                                reader.GetString(4)
                            ));
                        }
                    }
                }
            }
            return follows;
        }

        public Follow GetFollow(string sender, string receiver)
        {
            Follow follow = null;
            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT Sender, Receiver, IsCloseFriend, ExpirationTimeStamp, Description FROM Follows WHERE Sender = @Sender AND Receiver = @Receiver", connection))
                {
                    command.Parameters.AddWithValue("@Sender", sender);
                    command.Parameters.AddWithValue("@Receiver", receiver);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            follow = new Follow(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetBoolean(2),
                                reader.GetDateTime(3),
                                reader.GetString(4)
                            );
                        }
                    }
                }
            }
            return follow;
        }
    }
}
