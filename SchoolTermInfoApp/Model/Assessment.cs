﻿using System;
using SQLite;

namespace SchoolTermInfoApp.Model
{
    [Table("Assessment")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string AssessmentName { get; set; }
        public int CourseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string AssessmentType { get; set; }
        public int AssessmentNotifications { get; set; }
    }
}
