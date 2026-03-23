using Microsoft.Data.SqlClient;

namespace Razor
{
    public class Database()
    {
        public string ConnectionString { get; } = @"Server=(LocalDB)\Test;Integrated Security=true";

        public IEnumerable<User> GetUsers()
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand("SELECT Id, Username, About FROM Users", connection);

            connection.Open();
            using var reader = command.ExecuteReader();
            var users = new List<User>();

            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    About = reader.IsDBNull(2) ? null : reader.GetString(2)
                });
            }

            return users;
        }

        public User? GetUserById(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand("SELECT Id, Username, About FROM Users WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            return new User
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                About = reader.IsDBNull(2) ? null : reader.GetString(2)
            };
        }

        public void UpdateUser(User user)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand("UPDATE Users SET Username = @Username, About = @About WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@About", (object?)user.About ?? DBNull.Value);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}