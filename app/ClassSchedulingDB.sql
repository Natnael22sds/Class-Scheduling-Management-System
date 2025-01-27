-- Create database
CREATE DATABASE ClassSchedulingDB;
GO

-- Use the created database
USE ClassSchedulingDB;
GO

-- Create Users Table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Role NVARCHAR(50),
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- Create Courses Table
CREATE TABLE Courses (
    CourseId INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(100),
    Description NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO
INSERT INTO Courses( CourseName,  Description) 
VALUES
('C#', 'Good programming language')

SELECT * FROM Courses

-- Create Schedules Table
CREATE TABLE Schedules (
    ScheduleId INT PRIMARY KEY IDENTITY(1,1),
    CourseId INT FOREIGN KEY REFERENCES Courses(CourseId),
    StartTime DATETIME,
    EndTime DATETIME,
    Room NVARCHAR(50),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CHECK (StartTime < EndTime) -- Ensures start time is before end time
);
GO

-- Create Reports Table
CREATE TABLE Reports (
    ReportId INT PRIMARY KEY IDENTITY(1,1),
    ReportType NVARCHAR(50),
    GeneratedAt DATETIME DEFAULT GETDATE(),
    Content NVARCHAR(MAX)
);
GO


-- Stored Procedure to Add User
CREATE PROCEDURE AddUser (
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Email NVARCHAR(100),
    @Role NVARCHAR(50)
)
AS
BEGIN
    BEGIN TRY
        INSERT INTO Users (FirstName, LastName, Email, Role)
        VALUES (@FirstName, @LastName, @Email, @Role);
    END TRY
    BEGIN CATCH
        PRINT 'Error: ' + ERROR_MESSAGE();
        THROW; -- Rethrow the error for better debugging
    END CATCH
END;
GO

-- Stored Procedure to Get All Users
CREATE PROCEDURE GetAllUsers
AS
BEGIN
    BEGIN TRY
        SELECT * FROM Users;
    END TRY
    BEGIN CATCH
        PRINT 'Error: ' + ERROR_MESSAGE();
        THROW; -- Rethrow the error for better debugging
    END CATCH
END;
GO

-- Stored Procedure to Add Course
CREATE PROCEDURE AddCourse (
    @CourseName NVARCHAR(100),
    @Description NVARCHAR(500)
)
AS
BEGIN
    BEGIN TRY
        INSERT INTO Courses (CourseName, Description)
        VALUES (@CourseName, @Description);
    END TRY
    BEGIN CATCH
        PRINT 'Error: ' + ERROR_MESSAGE();
        THROW; -- Rethrow the error for better debugging
    END CATCH
END;
GO

-- Stored Procedure to Get All Courses
CREATE PROCEDURE GetAllCourses
AS
BEGIN
    BEGIN TRY
        SELECT * FROM Courses;
    END TRY
    BEGIN CATCH
        PRINT 'Error: ' + ERROR_MESSAGE();
        THROW; -- Rethrow the error for better debugging
    END CATCH
END;
GO

-- Stored Procedure to Generate Report
CREATE PROCEDURE GenerateReport (
    @ReportType NVARCHAR(50),
    @Content NVARCHAR(MAX)
)
AS
BEGIN
    BEGIN TRY
        INSERT INTO Reports (ReportType, Content)
        VALUES (@ReportType, @Content);
    END TRY
    BEGIN CATCH
        PRINT 'Error: ' + ERROR_MESSAGE();
        THROW; -- Rethrow the error for better debugging
    END CATCH
END;
GO

-- Trigger for preventing deletion of courses that are scheduled
CREATE TRIGGER PreventCourseDeletion
ON Courses
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Schedules WHERE CourseId IN (SELECT CourseId FROM deleted))
    BEGIN
        PRINT 'Cannot delete course as it is linked to an existing schedule.';
    END
    ELSE
    BEGIN
        DELETE FROM Courses WHERE CourseId IN (SELECT CourseId FROM deleted);
    END
END;
GO

-- Index on foreign key column for performance (optional)
CREATE INDEX IX_Schedules_CourseId ON Schedules(CourseId);
GO

-- Faculty Table
CREATE TABLE Faculty (
    FacultyID INT IDENTITY(1,1) PRIMARY KEY,
    FacultyName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE
);

GO

-- Rooms Table
CREATE TABLE Rooms (
    RoomID INT IDENTITY(1,1) PRIMARY KEY,
    RoomName NVARCHAR(255) NOT NULL,
    Capacity INT NOT NULL
);
GO
-- FacultyPreferences Table
CREATE TABLE FacultyPreferences (
    PreferenceID INT IDENTITY(1,1) PRIMARY KEY,
    FacultyID INT NOT NULL,
    PreferredDay NVARCHAR(20) NOT NULL,
    PreferredTimeSlot TIME NOT NULL,
    FOREIGN KEY (FacultyID) REFERENCES Faculty(FacultyID)
);

GO

-- TimeSlots Table
CREATE TABLE TimeSlots (
    TimeSlotID INT IDENTITY(1,1) PRIMARY KEY,
    Day NVARCHAR(20) NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL
);

GO
-- RoomUsageLog Table
CREATE TABLE RoomUsageLog (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    RoomID INT NOT NULL,
    TimeSlotID INT NOT NULL,
    UsageDate DATE NOT NULL,
    Purpose NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    FOREIGN KEY (TimeSlotID) REFERENCES TimeSlots(TimeSlotID)
);

GO

-- AuditLogs Table
CREATE TABLE AuditLogs (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    Action NVARCHAR(255) NOT NULL,
    PerformedBy NVARCHAR(255) NOT NULL,
    PerformedAt DATETIME DEFAULT GETDATE()
);

GO

