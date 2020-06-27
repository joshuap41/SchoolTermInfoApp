using System;
using System.Collections.Generic;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;
using Xamarin.Forms;
using SQLite;
using System.Linq;


namespace SchoolTermInfoApp.View
{
    public partial class CreateNewCoursePage : ContentPage
    {
        private Term selectedTerm;

        public CreateNewCoursePage(Term selectedTerm)
        {
            InitializeComponent();

            this.selectedTerm = selectedTerm;
        }

        //public int CountCheck(Term selectedTerm)
        //{
        //    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
        //    {
        //        conn.CreateTable<Course>();
        //        var courseTable = conn.Table<Course>().ToList();

        //        var listOfCourses = (from course in courseTable
        //                             where course.TermNumber == selectedTerm.Id
        //                             select course).ToList();

        //        var count = listOfCourses.Count();

        //        return count;
        //    }

        //}

        //save button
        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Course createCourse = new Course()
            {
                CourseName = courseName.Text,
                MentorName = mentorName.Text,
                MentorPhoneNumber = mentorPhoneNumber.Text,
                MentorEmail = mentorEmail.Text,
                CourseStatus = Convert.ToString(courseStatus.SelectedItem),
                StartDate = startDate.Date,
                FinishDate = finishDate.Date,
                CourseNotes = courseNotes.Text,
                TermNumber = selectedTerm.Id,

                //change to mosh example Udemy
                CourseNotificationsOn = courseNotifications.On == true ? 1 : 0
            };


            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();

                //checks courseList for selectedTerm
                int count =  App.CountCheck(selectedTerm);

                var rows = conn.Insert(createCourse);

                if (count <= 5 && rows > 0)
                {
                    DisplayAlert("Success", "New Course Added", "OK");
                }
                else
                {
                    DisplayAlert("Failure", "Only 6 Courses Allowed Per Term", "OK");
                }

                //Make sure that the finish date is greater than the start date

                //Check for nulls with the name and display an alert if it is bad

                //if(createTerm.StartDate < createTerm.FinishDate)
            }
            Navigation.PushAsync(new TermPage(selectedTerm));
        }
    }
}
