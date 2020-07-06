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

        
        void SaveButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
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
                CourseNotifications = courseNotifications.On == true ? 1 : 0
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();

                int count =  App.CourseCountCheck(selectedTerm);

                if (count <= 5)
                {
                    if (createCourse.StartDate < createCourse.FinishDate)
                    {
                        if (App.IsValidEmail(mentorEmail.Text))
                        {
                            if (courseName.Text == "" || mentorName.Text == "" || mentorPhoneNumber.Text == "" || Convert.ToString(courseStatus.SelectedItem) == "")
                            {
                                DisplayAlert("Failure", "Please provide all course and mentor information", "OK");
                            }
                            else
                            {
                                conn.Insert(createCourse);
                                DisplayAlert("Success", "Course successfully added", "OK");
                                Navigation.PushAsync(new TermPage(selectedTerm));
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
                else
                {
                    DisplayAlert("Failure", "Only 6 courses are allowed per term", "OK");
                }
            }
        }

        void HomeButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
