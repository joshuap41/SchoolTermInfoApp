﻿using SchoolTermInfoApp.Model;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
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

            courseName.Text = selectedCourse.CourseName;
            mentorName.Text = selectedCourse.MentorName;
            mentorPhoneNumber.Text = selectedCourse.MentorPhoneNumber;
            mentorEmail.Text = selectedCourse.MentorEmail;
            courseStatus.Text = selectedCourse.CourseStatus;
            startDate.Text = selectedCourse.StartDate.ToString(App.dateFormat);
            finishDate.Text = selectedCourse.FinishDate.ToString(App.dateFormat);
            courseNotes.Text = selectedCourse.CourseNotes;
            courseNotifications.Text = selectedCourse.CourseNotifications == 1 ? "Enabled" : "Disabled";
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

        private void HomeToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private async void ShareButton_Clicked_1(System.Object sender, System.EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = courseNotes.Text,
                Title = "Share your notes on the course"
            });
        }
    }
}
