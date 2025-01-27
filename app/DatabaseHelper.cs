using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ClassSchedulingManagementSystem
{
    public class DatabaseHelper
    {
        private string connectionString = "Server=DESKTOP-6KIFO9P\\SQLEXPRESS;Database=ClassSchedulingDB;User Id=nyTech;Password=nyn123;TrustServerCertificate=True;";

        // Test Database Connection
        public void TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Database connection successful!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection test failed: {ex.Message}");
            }
        }

        // Fetch data using stored procedure
        private List<T> ExecuteStoredProcedure<T>(string procedureName, SqlParameter[] parameters, Func<SqlDataReader, T> mapFunc)
        {
            List<T> result = new List<T>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procedureName, conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(mapFunc(reader));
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing stored procedure {procedureName}: {ex.Message}");
                }
            }
            return result;
        }

        // Get users from the database
        public List<User> GetUsers()
        {
            const string procedureName = "GetAllUsers";
            return ExecuteStoredProcedure(procedureName, null, reader => new User
            {
                Id = reader.GetInt32(reader.GetOrdinal("UserId")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                Role = reader.GetString(reader.GetOrdinal("Role"))
            });
        }

        // Get courses from the database
        public List<Course> GetCourses()
        {
            const string procedureName = "GetAllCourses";
            return ExecuteStoredProcedure(procedureName, null, reader => new Course
            {
                CourseId = reader.GetInt32(reader.GetOrdinal("CourseId")),
                CourseName = reader.GetString(reader.GetOrdinal("CourseName")),
                Description = reader.GetString(reader.GetOrdinal("Description"))
            });
        }

        // Get departments from the database
        public List<Department> GetDepartments()
        {
            const string query = "SELECT * FROM Departments";
            List<Department> departments = new List<Department>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            departments.Add(new Department
                            {
                                DepartmentId = (int)reader["DepartmentId"],
                                DepartmentName = reader["DepartmentName"].ToString(),
                                Description = reader["Description"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching departments: {ex.Message}");
                }
            }
            return departments;
        }

        // Get time slots from the database
        public List<TimeSlot> GetTimeSlots()
        {
            const string query = "SELECT * FROM TimeSlots";
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timeSlots.Add(new TimeSlot
                            {
                                TimeSlotId = (int)reader["TimeSlotId"],
                                Day = reader["Day"].ToString(),
                                StartTime = (TimeSpan)reader["StartTime"],
                                EndTime = (TimeSpan)reader["EndTime"]
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching time slots: {ex.Message}");
                }
            }
            return timeSlots;
        }

        public List<StudentEnrollment> GetEnrollments()
        {
            const string query = "SELECT * FROM StudentEnrollments";
            List<StudentEnrollment> enrollments = new List<StudentEnrollment>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    enrollments.Add(new StudentEnrollment
                    {
                        EnrollmentId = (int)reader["EnrollmentId"],
                        StudentId = (int)reader["StudentId"],
                        CourseId = (int)reader["CourseId"],
                        EnrollmentDate = (DateTime)reader["EnrollmentDate"]
                    });
                }
            }
            return enrollments;
        }

        // Get faculty preferences by faculty ID
        public List<FacultyPreference> GetFacultyPreferences(int facultyId)
        {
            string query = "SELECT * FROM FacultyPreferences WHERE FacultyID = @FacultyID";
            List<FacultyPreference> preferences = new List<FacultyPreference>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FacultyID", facultyId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            preferences.Add(new FacultyPreference
                            {
                                PreferenceID = reader.GetInt32(reader.GetOrdinal("PreferenceID")),
                                FacultyID = reader.GetInt32(reader.GetOrdinal("FacultyID")),
                                PreferredDay = reader.GetString(reader.GetOrdinal("PreferredDay")),
                                PreferredTimeSlot = reader.GetTimeSpan(reader.GetOrdinal("PreferredTimeSlot"))
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching faculty preferences: {ex.Message}");
                }
            }
            return preferences;
        }

        // Execute stored procedure for users or courses (generic)
        public void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procedureName, conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing stored procedure {procedureName}: {ex.Message}");
                }
            }
        }
    }
}
