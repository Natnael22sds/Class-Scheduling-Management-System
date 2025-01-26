using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ClassSchedulingManagementSystem
{
    public class DatabaseHelper
    {
        private string connectionString = "your_connection_string"; // Replace with your actual connection string

        // Method to execute a stored procedure with parameters
        public void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing stored procedure: {ex.Message}");
                    throw;
                }
            }
        }

        // Method to execute a query and return a DataTable
        public DataTable ExecuteQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing query: {ex.Message}");
                    throw;
                }
            }
        }

        // Method to execute a stored procedure and return a list of users
        public List<User> ExecuteStoredProcedureForUsers(string procedureName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<User> users = new List<User>();
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Role = reader.GetString(reader.GetOrdinal("Role"))
                        };
                        users.Add(user);
                    }
                    return users;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing stored procedure for users: {ex.Message}");
                    throw;
                }
            }
        }

        // Method to execute a stored procedure and return a list of courses
        public List<Course> ExecuteStoredProcedureForCourses(string procedureName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Course> courses = new List<Course>();
                    while (reader.Read())
                    {
                        Course course = new Course
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CourseName = reader.GetString(reader.GetOrdinal("CourseName")),
                            Description = reader.GetString(reader.GetOrdinal("Description"))
                        };
                        courses.Add(course);
                    }
                    return courses;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing stored procedure for courses: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
