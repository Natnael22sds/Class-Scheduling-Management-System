# 🗓️ Class Scheduling Management System – Hilcoe

A robust, role-based scheduling system developed to automate and optimize class scheduling at **Hilcoe College**. This application resolves scheduling conflicts, tracks faculty preferences, manages room usage, and improves academic coordination with announcements and reports.

---

## 🎯 Features

### 🔐 Authentication & Roles
- Secure login system
- Admin, Faculty, and Scheduler access levels

### 📅 Scheduling
- Auto-generate conflict-free schedules
- Manual schedule creation/editing
- Time slots and room availability handling

### 🧑‍🏫 Faculty Module
- View personal schedules
- Submit preferred time slots
- View announcements and changes

### 🏫 Admin/Scheduler Panel
- Add/Edit/Delete courses, instructors, rooms
- Generate reports on room usage and scheduling
- Audit logs for schedule changes
- Department-wise planning

### 📢 Communication
- In-app announcement board for schedule updates

---

## 🗂️ Project Structure

```bash
ClassSchedulerSystem/
├── Controllers/               # C# Controllers for logic handling
├── Models/                    # Entity models for database
├── Views/                     # Razor views (.cshtml) for frontend
├── Data/                      # Database context and migrations
├── Services/                  # Business logic and helpers
├── appsettings.json           # Database connection settings
├── Program.cs / Startup.cs    # App configuration
└── README.md                  # Project documentation

```
🛠️ Tech Stack
C# / .NET Core – Backend framework

Entity Framework Core – ORM for SQL Server

SQL Server – Relational database

Razor Pages / MVC – Frontend with dynamic views

---
⚙️ Setup Instructions
Clone the repository
```bash
git clone https://github.com/your-username/class-scheduler-system.git

```

Configure database

Update appsettings.json with your SQL Server connection string

Run migrations
```bash
dotnet ef database update
```

Run the app
```bash
dotnet run
```


