using SchoolTermInfoApp.Model;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SQLite;
using SchoolTermInfoApp.Model;
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

            

            courseName.Text = selectedCourse.CourseName;
            mentorName.Text = selectedCourse.MentorName;
            mentorPhoneNumber.Text = selectedCourse.MentorPhoneNumber;
            mentorEmail.Text = selectedCourse.MentorEmail;
            //convert.tostring() maybe
            courseStatus.SelectedItem = selectedCourse.CourseStatus;
            startDate.Date = selectedCourse.StartDate;
            finishDate.Date = selectedCourse.FinishDate;
            courseNotes.Text = selectedCourse.CourseNotes;
            //notifications here or in the edit???
        }
        
        void EditCourse_Clicked(System.Object sender, System.EventArgs e)
        {
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

        void RequiredAssessments_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }

        private void HomeToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
