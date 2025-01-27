using System;
public class StudentEnrollment
{
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public string StudentName { get; set; }  // Add this property
    public string CourseName { get; set; }   // Add this property
    public DateTime EnrollmentDate { get; set; }  // Add this property
}
