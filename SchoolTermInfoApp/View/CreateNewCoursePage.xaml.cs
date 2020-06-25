using System;
using System.Collections.Generic;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;
using Xamarin.Forms;
using SQLite;

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
                CourseNotifications = courseNotifications.
            };

             
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();
                var rows = conn.Insert(createCourse);


                if (rows > 0)
                {
                    DisplayAlert("Success", "New Course Added", "OK");
                }
                else
                {
                    DisplayAlert("Failure", "Course Failed to be Added", "OK");
                }

                //Make sure that the finish date is greater than the start date

                //Check for nulls with the name and display an alert if it is bad

                //if(createTerm.StartDate < createTerm.FinishDate)
                
            }
            Navigation.PushAsync(new TermPage(selectedTerm));
        }
    }
}
