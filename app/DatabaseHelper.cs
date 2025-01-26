using System;
using System.Data;
using Microsoft.Data.SqlClient; // New namespace

public class DatabaseHelper
{
    private string connectionString = "your_connection_string_here";

    public void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(procedureName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
