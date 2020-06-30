using System;
using System.Collections.Generic;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;

using Xamarin.Forms;

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
                AssessmentType = Convert.ToString(assessmentType.SelectedItem),
                StartDate = startDate.Date,
                FinishDate = finishDate.Date,
                CourseNumber = selectedCourse.Id,

                //notifications need work
                //AssessmentNotificationsOn = assessmentNotification1.On
            };

            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Assessment>();

                var ObjCount = App.ObjectiveAssessmentCountCheck(selectedCourse);

                var rows = conn.Insert(createAssessment);

                if (rows > 0)
                    DisplayAlert("Success", "Assessment Successfully Created", "Ok");
                else
                    DisplayAlert("Failure","Assessment Failed to Create","Ok");

            }
            Navigation.PushAsync(new RequiredAssessmentsPage(selectedCourse));
        }

        void HomeButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
