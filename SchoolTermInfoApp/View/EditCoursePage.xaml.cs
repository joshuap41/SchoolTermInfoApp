using SchoolTermInfoApp.Model;
using System;
using System.Collections.Generic;
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
            courseNotifications.On = Convert.ToBoolean(selectedCourse.CourseNotifications);
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
            selectedCourse.CourseNotifications = courseNotifications.On == true ? 1 : 0;
                
            
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();

                if (selectedCourse.StartDate < selectedCourse.FinishDate)
                {
                    if (App.IsValidEmail(mentorEmail.Text))
                    {
                        if (courseName.Text == "" || mentorName.Text == "" || mentorPhoneNumber.Text == "" || Convert.ToString(courseStatus.SelectedItem) == "")
                        {
                            DisplayAlert("Failure", "Please provide all course and mentor information", "OK");
                        }
                        else
                        {
                            conn.Update(selectedCourse);
                            DisplayAlert("Success", "Course successfully added", "OK");
                            Navigation.PushAsync(new CoursePage(selectedCourse, currentTerm));
                        }
                    }
                    else
                    {
                        DisplayAlert("Failure", "Please enter a valid email address", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Failure", "The start date cannot be after the finish date", "OK");
                }
            }
        }

        private void HomeButtonToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}