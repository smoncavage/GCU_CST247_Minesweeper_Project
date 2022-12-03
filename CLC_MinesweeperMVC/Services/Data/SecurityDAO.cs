﻿using System;
using System.Data.SqlClient;

namespace Recipe_Shop.Services.Data
{
    public class SecurityDAO
    {
        public bool FindByUser(User user)
        {

            //string connectionString = "Server=(localdb)\\MSSQLLocalDB; Initial Catalog=Test; Integrated Security=True; Trusted_Connection=yes;";
            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Initial Catalog=MinesweeperApp; Integrated Security=true; Trusted_Connection=yes;";
            string query = "SELECT * FROM dbo.Users WHERE username=@username AND password=@password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@username", user.USERNAME);
                command.Parameters.AddWithValue("@password", user.PASSWORD);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public bool AddUser(User user)
        {

            return false;
        }
    }
}