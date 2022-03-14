using Homelend.Domain.AppConfiguration;
using HomeLend.Domain.Entities;
using HomeLend.Domain.Interfaces.Repositories;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HomeLend.Infrastructure.Data.Repositories
{

    public class ClientRepository : IClientRepository
    {
        private readonly AppSettings _appSettings;
        public ClientRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<Client> GetByIdAsync(int clientId)
        {
            Client client = null;
            
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "select id, name from client where id = @id"
                };

                var parameter = new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = clientId };
                command.Parameters.Add(parameter);

                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    client = new Client
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["name"].ToString()
                    };
                }

                if (connection.State == ConnectionState.Open)  await connection.CloseAsync();
            }
            return client;
        }
    }
}
