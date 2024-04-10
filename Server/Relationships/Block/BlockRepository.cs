using System;
using Microsoft.Data.SqlClient;
using UBB_SE_2024_Gaborment.Database;

namespace UBB_SE_2024_Gaborment.Server.Relationships.Block
{
    internal class BlockRepository
    {
        private readonly ApplicationDatabaseContext _databaseHelper;
        private readonly Logger _logger;
        public BlockRepository(ApplicationDatabaseContext databaseHelper, Logger logger)
        {
            _databaseHelper = databaseHelper;
            _logger = logger;
    }

        public void AddBlock(Block blockToBeAdded)
        {
            try {
                using (var connection = _databaseHelper.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("INSERT INTO Blocks (Sender, Receiver, StartingTimeStamp, Reason) VALUES (@Sender, @Receiver, @StartingTimeStamp, @Reason)", connection))
                    {
                        command.Parameters.AddWithValue("@Sender", blockToBeAdded.getSender());
                        command.Parameters.AddWithValue("@Receiver", blockToBeAdded.getReceiver());
                        command.Parameters.AddWithValue("@StartingTimeStamp", blockToBeAdded.getStartingTimeStamp());
                        command.Parameters.AddWithValue("@Reason", blockToBeAdded.getReason());
                        command.ExecuteNonQuery();
                    }
                }
            } catch(Exception exception)
            {
                _logger.Log("ERROR", exception.Message);
            }
        }

        public void RemoveBlock(string sender, string receiver)
        {
            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM Blocks WHERE Sender = @Sender AND Receiver = @Receiver", connection))
                    {
                        command.Parameters.AddWithValue("@Sender", sender);
                        command.Parameters.AddWithValue("@Receiver", receiver);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.Log("ERROR", exception.Message);
            }
        }

        public List<Block> GetBlocksBySender(string sender)
        {
            var blocks = new List<Block>();
            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT Receiver, StartingTimeStamp, Reason FROM Blocks WHERE Sender = @Sender", connection))
                    {
                        command.Parameters.AddWithValue("@Sender", sender);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                blocks.Add(new Block
                                (
                                    sender,
                                    reader.GetString(0),
                                    reader.GetDateTime(1),
                                    reader.GetString(2)
                                ));
                            }
                        }
                    }
                }
            } catch (Exception exception)
            {
                _logger.Log("ERROR", exception.Message);
            }
            return blocks;
        }

        public List<Block> GetBlocksOfReceiver(string receiver)
        {
            var blocks = new List<Block>();
            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT Sender, StartingTimeStamp, Reason FROM Blocks WHERE Receiver = @Receiver", connection))
                    {
                        command.Parameters.AddWithValue("@Receiver", receiver);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                blocks.Add(new Block
                                (
                                    reader.GetString(0),
                                    receiver,
                                    reader.GetDateTime(1),
                                    reader.GetString(2)
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.Log("ERROR", exception.Message);
            }
            return blocks;
        }

        public List<Block> GetBlocks()
        {
            var blocks = new List<Block>();
            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT Sender, Receiver, StartingTimeStamp, Reason FROM Blocks", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                blocks.Add(new Block
                                (
                                    reader.GetString(0),
                                    reader.GetString(1),
                                    reader.GetDateTime(2),
                                    reader.GetString(3)
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.Log("ERROR", exception.Message);
            }
            return blocks;
        }
    }
}
