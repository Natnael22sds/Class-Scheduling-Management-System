using System;
using System.Collections.Generic;
using System.Data;
using ClassSchedulingManagementSystem;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ClassSchedulingManagementSystem
{
    public class MainForm : Form
    {
        private DatabaseHelper databaseHelper;

        public MainForm()
        {
            databaseHelper = new DatabaseHelper();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Class Scheduling Management System";
            this.ClientSize = new Size(1000, 700);
            this.MinimumSize = new Size(1000, 700);

            // Title Label
            Label titleLabel = new Label
            {
                Text = "Class Scheduling Management System",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.DarkSlateBlue,
                ForeColor = Color.White,
                Height = 70,
                Dock = DockStyle.Top
            };

            this.Controls.Add(titleLabel);

            // Navigation Panel
            Panel navigationPanel = new Panel
            {
                Width = 240,
                Dock = DockStyle.Left,
                BackColor = Color.LightSlateGray
            };

            // Content Panel
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            // Add navigation buttons to the navigation panel with fixed spacing
            int buttonSpacing = 10;
            int buttonHeight = 50;
            AddNavigationButton("Manage Users", navigationPanel, () => LoadManageUsers(contentPanel), buttonSpacing);
            buttonSpacing += buttonHeight + 10;
            AddNavigationButton("Manage Courses", navigationPanel, () => LoadManageCourses(contentPanel), buttonSpacing);
            buttonSpacing += buttonHeight + 10;
            AddNavigationButton("Manage Departments", navigationPanel, () => LoadManageDepartments(contentPanel), buttonSpacing);
            buttonSpacing += buttonHeight + 10;
            AddNavigationButton("Manage Time Slots", navigationPanel, () => LoadManageTimeSlots(contentPanel), buttonSpacing);
            buttonSpacing += buttonHeight + 10;
            AddNavigationButton("Manage Enrollments", navigationPanel, () => LoadManageEnrollments(contentPanel), buttonSpacing);
            buttonSpacing += buttonHeight + 10;
            AddNavigationButton("Generate Reports", navigationPanel, () => LoadReports(contentPanel), buttonSpacing);

            // Exit Button with Confirmation, placed at the bottom of the navigation panel
            Button btnExit = new Button
            {
                Text = "Exit",
                Width = 200,
                Height = 50,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                BackColor = Color.Firebrick,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(20, navigationPanel.Height + 520)
            };
            btnExit.Click += (s, e) => ConfirmExit();
            navigationPanel.Controls.Add(btnExit);

            this.Controls.Add(navigationPanel);
            this.Controls.Add(contentPanel);
        }

        private void AddNavigationButton(string text, Panel navigationPanel, Action onClick, int top)
        {
            Button button = new Button
            {
                Text = text,
                Width = navigationPanel.Width - 20,
                Height = 50,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(10, top)
            };
            button.Click += (s, e) => onClick();
            navigationPanel.Controls.Add(button);
        }

        private void LoadManageUsers(Panel contentPanel)
        {
            contentPanel.Controls.Clear();
            Label label = CreateSectionLabel("Manage Users");
            contentPanel.Controls.Add(label);

            ListBox userList = new ListBox
            {
                Width = contentPanel.Width - 20,
                Height = 210,
                Location = new Point(290, 70)
            };
            contentPanel.Controls.Add(userList);

            List<User> users = databaseHelper.GetUsers();
            foreach (var user in users)
            {
                userList.Items.Add($"{user.FirstName} {user.LastName} ({user.Email})");
            }

            Button addUserButton = new Button
            {
                Text = "Add User",
                Width = contentPanel.Width - 20,
                Height = 50,
                Location = new Point(10, 300),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            addUserButton.Click += (s, e) => ShowAddUserForm();
            contentPanel.Controls.Add(addUserButton);
        }

        private void LoadManageCourses(Panel contentPanel)
        {
            contentPanel.Controls.Clear();
            Label label = CreateSectionLabel("Manage Courses");
            contentPanel.Controls.Add(label);

            ListBox courseList = new ListBox
            {
                Width = contentPanel.Width - 20,
                Height = 200,
                Location = new Point(290, 70)
            };

            List<Course> courses = databaseHelper.GetCourses();
            foreach (var course in courses)
            {
                courseList.Items.Add($"{course.CourseName}: {course.Description}");
            }

            contentPanel.Controls.Add(courseList);
        }

        private void LoadManageDepartments(Panel contentPanel)
        {
            contentPanel.Controls.Clear();
            Label label = CreateSectionLabel("Manage Departments");
            contentPanel.Controls.Add(label);

            ListBox departmentList = new ListBox
            {
                Width = contentPanel.Width - 20,
                Height = 200,
                Location = new Point(10, 70)
            };

            List<Department> departments = databaseHelper.GetDepartments();
            foreach (var department in departments)
            {
                departmentList.Items.Add(department.DepartmentName);
            }

            contentPanel.Controls.Add(departmentList);
        }

        private void LoadManageTimeSlots(Panel contentPanel)
        {
            contentPanel.Controls.Clear();
            Label label = CreateSectionLabel("Manage Time Slots");
            contentPanel.Controls.Add(label);

            ListBox timeSlotList = new ListBox
            {
                Width = contentPanel.Width - 20,
                Height = 200,
                Location = new Point(10, 70)
            };

            List<TimeSlot> timeSlots = databaseHelper.GetTimeSlots();
            foreach (var timeSlot in timeSlots)
            {
                timeSlotList.Items.Add($"{timeSlot.StartTime} - {timeSlot.EndTime}");
            }

            contentPanel.Controls.Add(timeSlotList);
        }

        private void LoadManageEnrollments(Panel contentPanel)
        {
            contentPanel.Controls.Clear();
            Label label = CreateSectionLabel("Manage Enrollments");
            contentPanel.Controls.Add(label);

            // Enrollment management logic here
        }

        private void LoadReports(Panel contentPanel)
        {
            contentPanel.Controls.Clear();
            Label label = CreateSectionLabel("Generate Reports");
            contentPanel.Controls.Add(label);

            Button generateReportButton = new Button
            {
                Text = "Generate Report",
                Width = contentPanel.Width - 20,
                Height = 50,
                Location = new Point(10, 70),
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            generateReportButton.Click += (s, e) => MessageBox.Show("Report Generated Successfully!");

            contentPanel.Controls.Add(generateReportButton);
        }

        private Label CreateSectionLabel(string text)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 350,
                Height = 60,
                Location = new Point(10, 10),
                ForeColor = Color.MidnightBlue
            };
        }

        private void ConfirmExit()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Application.Exit();
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

            // Adjust the position of each TextBox control to avoid overlap
            TextBox firstNameTextBox = new TextBox
            {
                PlaceholderText = "First Name",
                Width = 350,
                Location = new Point(20, 20)  // Place it 20 pixels from the left
            };
            TextBox lastNameTextBox = new TextBox
            {
                PlaceholderText = "Last Name",
                Width = 350,
                Location = new Point(20, 60)  // Place it 20 pixels from the left and 40 pixels below the first box
            };
            TextBox emailTextBox = new TextBox
            {
                PlaceholderText = "Email",
                Width = 350,
                Location = new Point(20, 100)  // Place it 20 pixels from the left and 40 pixels below the second box
            };

            Button addButton = new Button
            {
                Text = "Add User",
                Width = 350,
                Height = 40,
                Location = new Point(20, 140)  // Place it 20 pixels from the left and 40 pixels below the third box
            };

            addButton.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(firstNameTextBox.Text) || string.IsNullOrWhiteSpace(lastNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(emailTextBox.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlParameter[] parameters = new SqlParameter[] 
                {
                    new SqlParameter("@FirstName", firstNameTextBox.Text),
                    new SqlParameter("@LastName", lastNameTextBox.Text),
                    new SqlParameter("@Email", emailTextBox.Text),
                    new SqlParameter("@Role", "User")
                };

                databaseHelper.ExecuteStoredProcedure("AddUser", parameters);
                MessageBox.Show("User added successfully!");
                addUserForm.Close();
            };

            addUserForm.Controls.Add(firstNameTextBox);
            addUserForm.Controls.Add(lastNameTextBox);
            addUserForm.Controls.Add(emailTextBox);
            addUserForm.Controls.Add(addButton);
            addUserForm.ShowDialog();
        }
    }
}
