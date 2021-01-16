using CLC_MinesweeperMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CLC_MinesweeperMVC.Services.Data {
    public class SecurityDAO {
        public bool FindByUser(UserModel user) {

            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Initial Catalog=MinesweeperApp; Integrated Security=true; Trusted_Connection=yes;";
            string query = "SELECT * FROM dbo.Users WHERE username=@username AND password=@password";

            using(SqlConnection connection = new SqlConnection(connectionString)) {

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@username", user.username);
                command.Parameters.AddWithValue("@password", user.password);
                try {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch(Exception ex) {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}