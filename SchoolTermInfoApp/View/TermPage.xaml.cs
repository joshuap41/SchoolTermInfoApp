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
        public TermPage(Term selectedTerm)
        {
            InitializeComponent();
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
           //need to build this first
        }

        void CourseListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //this is for the edit course page
        }
    }
}
