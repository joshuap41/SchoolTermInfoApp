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
        public CreateNewCoursePage()
        {
            InitializeComponent();
        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Course createCourse = new Course()
            {
                CourseName = courseName.Text,
                MentorName = mentorName.Text,
                MentorPhoneNumber = mentorPhoneNumber.Text(),
                MentorEmail = mentorEmail.Text,
                CourseStatus = courseStatus.ToString(),
                StartDate = startDate.Date,
                FinishDate = finishDate.Date,
                CourseNotes = courseNotes.Text,
                //CourseNotifications =
            };


            //****************have not tested this yet*******************
             
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();
                var rows = conn.Insert(createCourse);


                if (rows > 0)
                {
                    DisplayAlert("Success", "New Term Added", "OK");
                }
                else
                {
                    DisplayAlert("Failure", "Term Failed to be Added", "OK");
                }

                //Make sure that the finish date is greater than the start date

                //Check for nulls with the name and display an alert if it is bad

                //if(createTerm.StartDate < createTerm.FinishDate)

            }
            Navigation.PushAsync(new TermPage());
        }
    }
}
