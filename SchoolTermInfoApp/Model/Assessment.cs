using System;
using SQLite;

namespace SchoolTermInfoApp.Model
{
    [Table("Assessment")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public int CourseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string AssessmentType { get; set; }
        public bool AssessmentNotificationsOn { get; set; }
    }
}
