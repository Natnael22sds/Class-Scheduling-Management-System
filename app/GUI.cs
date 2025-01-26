using System;
using System.Data;
using Microsoft.Data.SqlClient; // New namespace
using System.Drawing;
using System.Windows.Forms;

namespace ClassSchedulingManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Class Scheduling Management System";
            this.ClientSize = new Size(1000, 700);
            this.MinimumSize = new Size(800, 600);

            // Title Label
            Label titleLabel = new Label();
            titleLabel.Text = "Class Scheduling Management System";
            titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Height = 60;
            titleLabel.BackColor = Color.MidnightBlue;
            titleLabel.ForeColor = Color.White;
            this.Controls.Add(titleLabel);

            // Navigation Panel
            Panel navigationPanel = new Panel();
            navigationPanel.Dock = DockStyle.Left;
            navigationPanel.Width = 220;
            navigationPanel.BackColor = Color.LightSteelBlue;
            this.Controls.Add(navigationPanel);

            // Content Panel
            Panel contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.WhiteSmoke;
            this.Controls.Add(contentPanel);

            // Navigation Buttons
            AddNavigationButton("Manage Users", navigationPanel, () => LoadManageUsers(contentPanel));
            AddNavigationButton("Manage Courses", navigationPanel, () => LoadManageCourses(contentPanel));
            AddNavigationButton("Generate Reports", navigationPanel, () => LoadReports(contentPanel));

            // Exit Button
            Button btnExit = new Button();
            btnExit.Text = "Exit";
            btnExit.Dock = DockStyle.Bottom;
            btnExit.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnExit.Height = 50;
            btnExit.BackColor = Color.Firebrick;
            btnExit.ForeColor = Color.White;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Click += (s, e) => Application.Exit();
            navigationPanel.Controls.Add(btnExit);
        }

        private void AddNavigationButton(string text, Panel navigationPanel, Action onClick)
        {
            Button button = new Button
            {
                Text = text,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Height = 50,
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            button.Click += (s, e) => onClick();
            navigationPanel.Controls.Add(button);
        }

        private void LoadManageUsers(Panel contentPanel)
        {
            contentPanel.Controls.Clear();
            Label label = CreateSectionLabel("Manage Users");
            contentPanel.Controls.Add(label);

            // Sample User Management Functionality
            ListBox userList = new ListBox
            {
                Dock = DockStyle.Top,
                Height = 200,
                Items = { "User1", "User2", "User3" }
            };
            contentPanel.Controls.Add(userList);

            Button addUserButton = new Button
            {
                Text = "Add User",
                Dock = DockStyle.Top,
                Height = 40
            };
            addUserButton.Click += (s, e) => 
            {
                // Call AddUser method on button click
                string firstName = "John";  // Example data
                string lastName = "Doe";    // Example data
                string email = "john.doe@example.com";  // Example data
                string role = "Student";  // Example data
                AddUser(firstName, lastName, email, role);
                MessageBox.Show("User added successfully!");
            };
            contentPanel.Controls.Add(addUserButton);
        }

        private void LoadManageCourses(Panel contentPanel)
        {
            contentPanel.Controls.Clear();
            Label label = CreateSectionLabel("Manage Courses");
            contentPanel.Controls.Add(label);

            // Sample Course Management Functionality
            ListBox courseList = new ListBox
            {
                Dock = DockStyle.Top,
                Height = 200,
                Items = { "Course1", "Course2", "Course3" }
            };
            contentPanel.Controls.Add(courseList);

            Button addCourseButton = new Button
            {
                Text = "Add Course",
                Dock = DockStyle.Top,
                Height = 40
            };
            addCourseButton.Click += (s, e) => 
            {
                // Call AddCourse method on button click
                string courseName = "Math 101";  // Example data
                string description = "Basic Mathematics";  // Example data
                AddCourse(courseName, description);
                MessageBox.Show("Course added successfully!");
            };
            contentPanel.Controls.Add(addCourseButton);
        }

        private void LoadReports(Panel contentPanel)
        {
            contentPanel.Controls.Clear();
            Label label = CreateSectionLabel("Generate Reports");
            contentPanel.Controls.Add(label);

            // Sample Reports Generation Functionality
            Button generateButton = new Button
            {
                Text = "Generate Report",
                Dock = DockStyle.Top,
                Height = 40
            };
            generateButton.Click += (s, e) => 
            {
                // Call GenerateReport method on button click
                string reportType = "Class Schedule";  // Example data
                string content = "Generated report for classes.";  // Example data
                GenerateReport(reportType, content);
                MessageBox.Show("Report generated successfully!");
            };
            contentPanel.Controls.Add(generateButton);
        }

        private Label CreateSectionLabel(string text)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 60,
                ForeColor = Color.MidnightBlue
            };
        }

        // Call the methods from DatabaseHelper
        public void AddUser(string firstName, string lastName, string email, string role)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@FirstName", firstName),
                new SqlParameter("@LastName", lastName),
                new SqlParameter("@Email", email),
                new SqlParameter("@Role", role)
            };

            DatabaseHelper dbHelper = new DatabaseHelper();
            dbHelper.ExecuteStoredProcedure("AddUser", parameters);
        }

        public void AddCourse(string courseName, string description)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@CourseName", courseName),
                new SqlParameter("@Description", description)
            };

            DatabaseHelper dbHelper = new DatabaseHelper();
            dbHelper.ExecuteStoredProcedure("AddCourse", parameters);
        }

        public void GenerateReport(string reportType, string content)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@ReportType", reportType),
                new SqlParameter("@Content", content)
            };

            DatabaseHelper dbHelper = new DatabaseHelper();
            dbHelper.ExecuteStoredProcedure("GenerateReport", parameters);
        }
    }
}
