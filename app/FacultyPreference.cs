using System;

namespace ClassSchedulingManagementSystem
{
    public class FacultyPreference
    {
        public int PreferenceID { get; set; }
        public int FacultyID { get; set; }
        public string PreferredDay { get; set; }
        public TimeSpan PreferredTimeSlot { get; set; }
    }
}
