using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

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
            titleLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Height = 70;
            titleLabel.BackColor = Color.DarkSlateBlue;
            titleLabel.ForeColor = Color.White;
            this.Controls.Add(titleLabel);

            // Navigation Panel
            Panel navigationPanel = new Panel();
            navigationPanel.Dock = DockStyle.Left;
            navigationPanel.Width = 220;
            navigationPanel.BackColor = Color.LightSlateGray;
            this.Controls.Add(navigationPanel);

            // Content Panel
            Panel contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.White;
            this.Controls.Add(contentPanel);

            // Navigation Buttons with Icons
            AddNavigationButton("Manage Users", navigationPanel, () => LoadManageUsers(contentPanel));
            AddNavigationButton("Manage Courses", navigationPanel, () => LoadManageCourses(contentPanel));
            AddNavigationButton("Generate Reports", navigationPanel, () => LoadReports(contentPanel));

            // Exit Button with Confirmation
            Button btnExit = new Button();
            btnExit.Text = "Exit";
            btnExit.Dock = DockStyle.Bottom;
            btnExit.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnExit.Height = 50;
            btnExit.BackColor = Color.Firebrick;
            btnExit.ForeColor = Color.White;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Click += (s, e) => ConfirmExit();
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

            // User Management ListBox
            ListBox userList = new ListBox
            {
                Dock = DockStyle.Top,
                Height = 200
            };
            contentPanel.Controls.Add(userList);

            // Fetch and Display Users from Database
            GetAllUsers(userList);

            // Add User Button with Icon
            Button addUserButton = new Button
            {
                Text = "Add User",
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            addUserButton.Click += (s, e) =>
            {
                ShowAddUserForm();
            };
            contentPanel.Controls.Add(addUserButton);
        }

        private void ShowAddUserForm()
        {
            Form addUserForm = new Form
            {
                Text = "Add User",
                ClientSize = new Size(400, 300),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };

            // Form Fields
            TextBox firstNameTextBox = new TextBox { PlaceholderText = "First Name", Dock = DockStyle.Top };
            TextBox lastNameTextBox = new TextBox { PlaceholderText = "Last Name", Dock = DockStyle.Top };
            TextBox emailTextBox = new TextBox { PlaceholderText = "Email", Dock = DockStyle.Top };
            ComboBox roleComboBox = new ComboBox
            {
                Dock = DockStyle.Top,
                Items = { "Student", "Instructor", "Administrator" }
            };

            Button addButton = new Button
            {
                Text = "Add User",
                Dock = DockStyle.Bottom
            };

            addButton.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(firstNameTextBox.Text) || string.IsNullOrWhiteSpace(lastNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(emailTextBox.Text) || roleComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AddUser(firstNameTextBox.Text, lastNameTextBox.Text, emailTextBox.Text, roleComboBox.SelectedItem.ToString());
                addUserForm.Close();
                MessageBox.Show("User added successfully!");

                // Reload Users
                LoadManageUsers((Panel)addUserForm.Owner.Controls["contentPanel"]);
            };

            addUserForm.Controls.Add(addButton);
            addUserForm.Controls.Add(roleComboBox);
            addUserForm.Controls.Add(emailTextBox);
            addUserForm.Controls.Add(lastNameTextBox);
            addUserForm.Controls.Add(firstNameTextBox);
            addUserForm.ShowDialog();
        }

        private void GetAllUsers(ListBox userList)
        {
            SqlParameter[] parameters = {};
            DatabaseHelper dbHelper = new DatabaseHelper();
            List<User> users = dbHelper.ExecuteStoredProcedureForUsers("GetAllUsers", parameters); // Ensure this method returns a list of users

            userList.Items.Clear();
            foreach (var user in users)
            {
                userList.Items.Add($"{user.FirstName} {user.LastName} ({user.Email})");
            }
        }

        private void LoadManageCourses(Panel contentPanel)
        {
            contentPanel.Controls.Clear();
            Label label = CreateSectionLabel("Manage Courses");
            contentPanel.Controls.Add(label);

            // Course Management ListBox
            ListBox courseList = new ListBox
            {
                Dock = DockStyle.Top,
                Height = 200
            };
            contentPanel.Controls.Add(courseList);

            // Fetch and Display Courses from Database
            GetAllCourses(courseList);

            // Add Course Button with Icon
            Button addCourseButton = new Button
            {
                Text = "Add Course",
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.CornflowerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            addCourseButton.Click += (s, e) =>
            {
                ShowAddCourseForm();
            };
            contentPanel.Controls.Add(addCourseButton);
        }

        private void ShowAddCourseForm()
        {
            Form addCourseForm = new Form
            {
                Text = "Add Course",
                ClientSize = new Size(400, 250),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };

            // Form Fields
            TextBox courseNameTextBox = new TextBox { PlaceholderText = "Course Name", Dock = DockStyle.Top };
            TextBox descriptionTextBox = new TextBox { PlaceholderText = "Description", Dock = DockStyle.Top };

            Button addButton = new Button
            {
                Text = "Add Course",
                Dock = DockStyle.Bottom
            };

            addButton.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(courseNameTextBox.Text) || string.IsNullOrWhiteSpace(descriptionTextBox.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AddCourse(courseNameTextBox.Text, descriptionTextBox.Text);
                addCourseForm.Close();
                MessageBox.Show("Course added successfully!");

                // Reload Courses
                LoadManageCourses((Panel)addCourseForm.Owner.Controls["contentPanel"]);
            };

            addCourseForm.Controls.Add(addButton);
            addCourseForm.Controls.Add(descriptionTextBox);
            addCourseForm.Controls.Add(courseNameTextBox);
            addCourseForm.ShowDialog();
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


        private void GetAllCourses(ListBox courseList)
        {
            SqlParameter[] parameters = {};
            DatabaseHelper dbHelper = new DatabaseHelper();
            List<Course> courses = dbHelper.ExecuteStoredProcedureForCourses("GetAllCourses", parameters); // Ensure this method returns a list of courses

            courseList.Items.Clear();
            foreach (var course in courses)
            {
                courseList.Items.Add($"{course.CourseName}: {course.Description}");
            }
        }

        private void LoadReports(Panel contentPanel)
        {
            contentPanel.Controls.Clear();
            Label label = CreateSectionLabel("Generate Reports");
            contentPanel.Controls.Add(label);

            // Report Generation Button with Icon
            Button generateButton = new Button
            {
                Text = "Generate Report",
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            generateButton.Click += (s, e) =>
            {
                GenerateReport("Class Schedule", "Generated report for classes.");
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

        // Exit Confirmation
        private void ConfirmExit()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Application.Exit();
        }

        // Database Methods
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
    }
}
