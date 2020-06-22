using System;
using System.Collections.Generic;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;

using Xamarin.Forms;

namespace SchoolTermInfoApp.View
{
    public partial class TermPage : ContentPage
    {
        public static Term currentTerm;

        public TermPage(Term selectedTerm)
        {
            InitializeComponent();

            currentTerm = selectedTerm;


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();
                var courses = conn.Table<Course>().ToList();
                courseListView.ItemsSource = courses;
            }
        }


        void CreateNewCourse_Clicked(System.Object sender, System.EventArgs e)
        {
            //Navigation.PushAsync(new CreateNewCoursePage());
        }

        void CourseListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //var selectedTerm = courseListView.SelectedItem as Term;
        }

        void EditTerm_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EditTermPage(currentTerm));
        }
    }
}
