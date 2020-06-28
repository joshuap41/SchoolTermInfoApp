using System;
using System.Collections.Generic;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;

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
                var assessments = conn.Table<Assessment>().ToList();
                assessmentsListView.ItemsSource = assessments;
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
            //Need to restrict to 1 PA and 1 OA
            Navigation.PushAsync(new CreateNewAssessmentPage(selectedCourse));
        }




        void HomeButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
