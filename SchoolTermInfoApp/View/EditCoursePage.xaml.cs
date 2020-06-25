using SchoolTermInfoApp.Model;
using System;
using System.Collections.Generic;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;
using Xamarin.Forms;
using SQLite;


namespace SchoolTermInfoApp.View
{
    public partial class EditCoursePage : ContentPage
    {
        Course selectedCourse;
        Term currentTerm;
        public EditCoursePage(Course selectedCourse, Term currentTerm)
        {
            InitializeComponent();

            this.selectedCourse = selectedCourse;
            this.currentTerm = currentTerm;

            courseName.Text = selectedCourse.CourseName;
            mentorName.Text = selectedCourse.MentorName;
            mentorPhoneNumber.Text = selectedCourse.MentorPhoneNumber;
            mentorEmail.Text = selectedCourse.MentorEmail;
            courseStatus.SelectedItem = selectedCourse.CourseStatus;
            startDate.Date = selectedCourse.StartDate;
            finishDate.Date = selectedCourse.FinishDate;
            courseNotes.Text = selectedCourse.CourseNotes;
            //notifications here
        }

        

        private void SaveButtonToolbarItem_Clicked(object sender, EventArgs e)
        {
            selectedCourse.CourseName = courseName.Text;
            selectedCourse.MentorName = mentorName.Text;
            selectedCourse.MentorPhoneNumber = mentorPhoneNumber.Text;
            selectedCourse.MentorEmail = mentorEmail.Text;
            selectedCourse.CourseStatus = Convert.ToString(courseStatus.SelectedItem);
            selectedCourse.StartDate = startDate.Date;
            selectedCourse.FinishDate = finishDate.Date;
            selectedCourse.CourseNotes = courseNotes.Text;
            //notifications here

            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();
                int rows = conn.Update(selectedCourse);

                if (rows > 0)
                    DisplayAlert("Success", "Course successfully updated", "Ok");
                else
                    DisplayAlert("Failure", "Course failed to update", "Ok");
            }
            Navigation.PushAsync(new TermPage(currentTerm));
        }


        private void HomeButtonToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
