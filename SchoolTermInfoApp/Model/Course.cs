using System;
using SQLite;

namespace SchoolTermInfoApp.Model
{
    [Table("Course")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // should I track this???
        //public int TermNumnber { get; set; }

        [MaxLength(255)]
        //replace term number with the ID
        public int TermNumber { get; set; }
        public string CourseName { get; set; }
        public string MentorName { get; set; }
        public string MentorPhoneNumber { get; set; }
        public string MentorEmail { get; set; }
        public string CourseStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string CourseNotes { get; set; }
        public bool CourseNotifications { get; set; }
    }
}
