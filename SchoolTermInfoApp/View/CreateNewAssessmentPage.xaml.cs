﻿using System;
using Xamarin.Forms;
using System.Collections.Generic;
using SQLite;
using SchoolTermInfoApp.Model;


namespace SchoolTermInfoApp.View
{
    public partial class CreateNewAssessmentPage : ContentPage
    {
        private Course selectedCourse;

        public CreateNewAssessmentPage(Course selectedCourse)
        {
            InitializeComponent();
            this.selectedCourse = selectedCourse;
        }

        void SaveButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Assessment createAssessment = new Assessment()
            {
                AssessmentName = assessmentName.Text,
                AssessmentType = Convert.ToString(assessmentType.SelectedItem),
                StartDate = startDate.Date,
                FinishDate = finishDate.Date,
                CourseNumber = selectedCourse.Id,
                AssessmentNotifications = assessmentNotifications1.On == true ? 1 : 0
            };



            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Assessment>();

                var ObjCount = App.ObjectiveAssessmentCountCheck(selectedCourse);

                var PerCount = App.PerformanceAssessmentCountCheck(selectedCourse);

                var Type = Convert.ToString(assessmentType.SelectedItem);


                if (ObjCount > 0 && Type == "Objective Assessment")
                {
                    DisplayAlert("Failure", "Only 1 Objective Assessment is allowed per course", "Ok");
                }
                else if (PerCount > 0 && Type == "Performance Assessment")
                {
                    DisplayAlert("Failure", "Only 1 Performance Assessment is allowed per course", "Ok");
                }
                else if (createAssessment.StartDate < createAssessment.FinishDate)
                {
                    if (string.IsNullOrWhiteSpace(assessmentName.Text) ||
                        string.IsNullOrWhiteSpace(Convert.ToString(assessmentType.SelectedItem)))
                    {
                        DisplayAlert("Failure", "Please provide all assessment information", "OK");
                    }
                    else
                    {
                        conn.Insert(createAssessment);
                        DisplayAlert("Success", "Assessment has been successfully created", "Ok");
                        Navigation.PushAsync(new RequiredAssessmentsPage(selectedCourse));
                    }
                }
                else
                {
                    DisplayAlert("Failure", "The start date cannot be after the finish date", "OK");
                }
            }
        }

        void HomeButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
