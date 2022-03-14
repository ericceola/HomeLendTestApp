using Homelend.Domain.AppConfiguration;
using HomeLend.Domain.Entities;
using HomeLend.Domain.Interfaces.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HomeLend.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;
        public UserRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task AddAsync(User user)
        {
           
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = System.Data.CommandType.Text,
                    CommandText =
                        "insert into users " +
                        " (clientId, DateOfBirth, FirstName, LastName, EmailAddress, HasCreditLimit, CreditLimit) " +
                        " values " +
                        " (@clientId, @DateOfBirth, @FirstName, @LastName, @EmailAddress, @HasCreditLimit, @CreditLimit) "
                };

                command.Parameters.Add(new SqlParameter("@clientId", System.Data.SqlDbType.Int) { Value = user.ClientId });
                command.Parameters.Add(new SqlParameter("@DateOfBirth", System.Data.SqlDbType.DateTime) { Value = user.DateOfBirth });
                command.Parameters.Add(new SqlParameter("@FirstName", System.Data.SqlDbType.VarChar) { Value = user.FirstName });
                command.Parameters.Add(new SqlParameter("@LastName", System.Data.SqlDbType.VarChar) { Value = user.LastName });
                command.Parameters.Add(new SqlParameter("@EmailAddress", System.Data.SqlDbType.VarChar) { Value = user.EmailAddress });
                command.Parameters.Add(new SqlParameter("@HasCreditLimit", System.Data.SqlDbType.Int) { Value = user.HasCreditLimit ? 1 : 0 });
                command.Parameters.Add(new SqlParameter("@CreditLimit", System.Data.SqlDbType.Int) { Value = user.CreditLimit });

                await connection.OpenAsync();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {

                    try
                    {
                        command.Transaction = transaction;
                        await command.ExecuteNonQueryAsync();

                        transaction.Commit();

                        if (connection.State == ConnectionState.Open) await connection.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Error data base. {ex.Message}");
                    }
                   
                }
            }
        }
    }
}
