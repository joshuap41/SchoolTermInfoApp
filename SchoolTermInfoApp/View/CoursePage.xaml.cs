using SchoolTermInfoApp.Model;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SQLite;
using SchoolTermInfoApp.View;

namespace SchoolTermInfoApp.View
{
    
    public partial class CoursePage : ContentPage
    {
        private Course selectedCourse;
        private Term currentTerm;
        

        public CoursePage(Course selectedCourse, Term currentTerm)
        {
            InitializeComponent();

            this.selectedCourse = selectedCourse;
            this.currentTerm = currentTerm;

            //change to binding in the UI
            courseName.Text = selectedCourse.CourseName;
            mentorName.Text = selectedCourse.MentorName;
            mentorPhoneNumber.Text = selectedCourse.MentorPhoneNumber;
            mentorEmail.Text = selectedCourse.MentorEmail;
            courseStatus.Text = selectedCourse.CourseStatus;
            startDate.Text = selectedCourse.StartDate.ToString(App.dateFormat);
            finishDate.Text = selectedCourse.FinishDate.ToString(App.dateFormat);
            courseNotes.Text = selectedCourse.CourseNotes;
            //change this around to look better with the switch
            courseNotifications.Text = selectedCourse.CourseNotificationsOn == 1 ? "Enabled" : "Disabled";
        }

        void RequiredAssessments_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new RequiredAssessmentsPage(selectedCourse));
        }
        
        void EditCourse_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EditCoursePage(selectedCourse, currentTerm));
        }

        void DeleteCourse_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();
                int rows = conn.Delete(selectedCourse);

                if (rows > 0)
                    DisplayAlert("Success", "Course successfully deleted", "Ok");
                else
                    DisplayAlert("Failure", "Course failed to delete", "Ok");
            }
            Navigation.PushAsync(new TermPage(currentTerm));
        }

        //the old save button
        //private void ToolbarItem_Clicked(object sender, EventArgs e)
        //{
        
        //}

        private void HomeToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
