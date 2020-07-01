using System;
using System.Collections.Generic;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;


using Xamarin.Forms;

namespace SchoolTermInfoApp.View
{
    public partial class EditAssessmentPage : ContentPage
    {
        Course selectedCourse;
        Assessment selectedAssessment;

        public EditAssessmentPage(Course selectedCourse, Assessment selectedAssessment)
        {
            InitializeComponent();

            this.selectedCourse = selectedCourse;
            this.selectedAssessment = selectedAssessment;

            assessmentType.SelectedItem = selectedAssessment.AssessmentType;
            startDate.Date = selectedAssessment.StartDate;
            finishDate.Date = selectedAssessment.FinishDate;
            selectedCourse.Id = selectedAssessment.CourseNumber;

            //bools and ints.... again...
            //assessmentNotification = currentAssessment.AssessmentNotificationsOn;

        }

        void HomeButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }







        void SaveButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            selectedAssessment.AssessmentType = Convert.ToString(assessmentType.SelectedItem);
            selectedAssessment.StartDate = startDate.Date;
            selectedAssessment.FinishDate = finishDate.Date;
            selectedAssessment.CourseNumber = selectedCourse.Id;

            //Notifications here
            //selectedAssessment.AssessmentNotificationsOn = assessmentNotification1.IsEnabled;


            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Assessment>();

                var ObjCount = App.ObjectiveAssessmentCountCheck(selectedCourse);

                var PerCount = App.PerformanceAssessmentCountCheck(selectedCourse);

                var Type = Convert.ToString(assessmentType.SelectedItem);

                //var Changed = assessmentType.SelectedIndexChanged;


                if (ObjCount == 1 && )
                {
                    DisplayAlert("Failure", "Only 1 Objective Assessment is allowed per course", "Ok");
                }
                else if (PerCount == 1 && Type == "Performance Assessment")
                {
                    DisplayAlert("Failure", "Only 1 Performance Assessment is allowed per course", "Ok");
                }
                else
                {
                    conn.Update(selectedAssessment);
                    DisplayAlert("Success", "Assessment has been successfully created", "Ok");
                }
            }
            Navigation.PushAsync(new RequiredAssessmentsPage(selectedCourse));
        }
    }
}
