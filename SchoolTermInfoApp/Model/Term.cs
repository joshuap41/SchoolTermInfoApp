using System;
using SQLite;

namespace SchoolTermInfoApp.Model
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string TermName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}
