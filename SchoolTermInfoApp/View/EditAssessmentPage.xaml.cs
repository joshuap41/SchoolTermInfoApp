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
        Course currentCourse;
        Assessment currentAssessment;

        public EditAssessmentPage(Course currentCourse, Assessment currentAssessment)
        {
            InitializeComponent();

            this.currentCourse = currentCourse;
            this.currentAssessment = currentAssessment;

            assessmentName.Text = currentAssessment.AssessmentName;
            assessmentType.SelectedItem = currentAssessment.AssessmentType;
            startDate.Date = currentAssessment.StartDate;
            finishDate.Date = currentAssessment.FinishDate;
            currentCourse.Id = currentAssessment.CourseNumber;

            //bools and ints.... again...
            //assessmentNotification = currentAssessment.AssessmentNotificationsOn;

        }

        void HomeButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void SaveButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
