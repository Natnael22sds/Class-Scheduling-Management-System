using System;

namespace ClassSchedulingManagementSystem
{
    public class TimeSlot
    {
        public int TimeSlotId { get; set; }
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
