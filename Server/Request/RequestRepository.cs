using System;
using Azure.Core;
using Microsoft.Data.SqlClient;
using UBB_SE_2024_Gaborment.Database;


namespace UBB_SE_2024_Gaborment.Server.Request
{
    internal class RequestRepository
    {
        private readonly ApplicationDatabaseContext _databaseHelper;
        private readonly Logger _logger;

        public RequestRepository(ApplicationDatabaseContext databaseHelper, Logger logger)
        {
            _databaseHelper = databaseHelper;
            _logger = logger;
        }

        public void AddRequest(Request request)
        {
            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("INSERT INTO Requests (Sender, Receiver) VALUES (@Sender, @Receiver)", connection))
                    {
                        command.Parameters.AddWithValue("@Sender", request.getSender());
                        command.Parameters.AddWithValue("@Receiver", request.getReceiver());
                        command.ExecuteNonQuery();
                    }
                }
            } catch (Exception exception)
            {
                _logger.Log("ERROR", exception.Message);
            }
        }

        public void RemoveRequest(string sender, string receiver)
        {
            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM Requests WHERE Sender = @Sender AND Receiver = @Receiver", connection))
                    {
                        command.Parameters.AddWithValue("@Sender", sender);
                        command.Parameters.AddWithValue("@Receiver", receiver);
                        command.ExecuteNonQuery();
                    }
                }
            } catch(Exception exception)
            {
                _logger.Log("ERROR", exception.Message);
            }
        }

        public List<Request> GetRequestsOf(string sender)
        {
            var requests = new List<Request>();
            try
            {

                using (var connection = _databaseHelper.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT Receiver FROM Requests WHERE Sender = @Sender", connection))
                    {
                        command.Parameters.AddWithValue("@Sender", sender);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                requests.Add(new Request(sender, reader.GetString(0)));
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.Log("ERROR", exception.Message);
            }
            return requests;
        }

        public List<Request> GetRequestsTo(string receiver)
        {
            var requests = new List<Request>();
            try { 
            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT Sender FROM Requests WHERE Receiver = @Receiver", connection))
                {
                    command.Parameters.AddWithValue("@Receiver", receiver);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            requests.Add(new Request(reader.GetString(0), receiver));
                        }
                    }
                }
            }
            
            }
            catch (Exception exception)
            {
                _logger.Log("ERROR", exception.Message);
            }
            return requests;
        }

        public Request GetRequest(string sender, string receiver)
        {
            Request request = null;
            try { 
                using (var connection = _databaseHelper.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT Sender, Receiver FROM Requests WHERE Sender = @Sender AND Receiver = @Receiver", connection))
                    {
                        command.Parameters.AddWithValue("@Sender", sender);
                        command.Parameters.AddWithValue("@Receiver", receiver);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                request = new Request(sender, receiver);
                            }
                        }
                    }
                }
                
            }
            catch (Exception exception)
            {
                _logger.Log("ERROR", exception.Message);
            }
            return request;
        }
    }
}
