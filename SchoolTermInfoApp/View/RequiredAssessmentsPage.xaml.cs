using System;
using System.Collections.Generic;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;
using System.Linq;

using Xamarin.Forms;

namespace SchoolTermInfoApp.View
{
    public partial class RequiredAssessmentsPage : ContentPage
    {
        private Course selectedCourse;

        public RequiredAssessmentsPage(Course selectedCourse)
        {
            InitializeComponent();

            this.selectedCourse = selectedCourse;            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Assessment>();
                var assessmentTable = conn.Table<Assessment>().ToList();

                //queries the list to display only the assessments for the selectedCourse
                var listOfAssessments = (from assessment in assessmentTable
                                         where assessment.CourseNumber == selectedCourse.Id
                                         select assessment).ToList();

                assessmentsListView.ItemsSource = listOfAssessments;
            }
        }

        void AssessmentsListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedAssessment = assessmentsListView.SelectedItem as Assessment;

            if (selectedAssessment != null)
                Navigation.PushAsync(new AssessmentPage(selectedAssessment, selectedCourse));
        }

        void CreateNewAssessment_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CreateNewAssessmentPage(selectedCourse));
        }

        void HomeButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
