using System;
using SQLite;

namespace SchoolTermInfoApp.Model
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string CourseName { get; set; }
        public string MentorName { get; set; }
        public int MentorPhoneNumber { get; set; }
        public string MentorEmail { get; set; }
        public string CourseStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string CourseNotes { get; set; }
        public int CourseNotifications { get; set; }
    }
}
