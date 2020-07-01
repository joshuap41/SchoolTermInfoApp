using System;
using System.Collections.Generic;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;


using Xamarin.Forms;

namespace SchoolTermInfoApp.View
{
    public partial class AssessmentPage : ContentPage
    {
        private Assessment selectedAssessment;
        private Course selectedCourse;

        public AssessmentPage(Assessment selectedAssessment, Course selectedCourse)
        {
            InitializeComponent();

            this.selectedAssessment = selectedAssessment;
            this.selectedCourse = selectedCourse;

            assessmentName.Text = selectedAssessment.AssessmentName;
            assessmentType.Text = Convert.ToString(selectedAssessment.AssessmentType);
            startDate.Text = selectedAssessment.StartDate.ToString(App.dateFormat);
            finishDate.Text = selectedAssessment.FinishDate.ToString(App.dateFormat);

            //Notification project
            //assessmentNotifications.Text = selectedAssessment.AssessmentNotificationsOn == 1 ? "Enabled" : "Disabled";
        }





        void HomeButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        void editAssessment_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EditAssessmentPage(selectedCourse, selectedAssessment));
        }

        void deleteAssessment_Clicked(System.Object sender, System.EventArgs e)
        {
            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Assessment>();
                int rows = conn.Delete(selectedAssessment);

                if (rows > 0)
                    DisplayAlert("Success","Assessment successfully deleted","Ok");
                else
                    DisplayAlert("Failure", "Assessment failed to delete", "Ok");
            }
            Navigation.PushAsync(new RequiredAssessmentsPage(selectedCourse));
        }
    }
}
