using System;
using System.Collections.Generic;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace SchoolTermInfoApp.View
{
    public partial class TermPage : ContentPage
    {
        //need to keep the members of the class private and access through properties, Etc.
        private Term currentTerm;
        private ObservableCollection<Course> listOfCourses;


        public TermPage(Term selectedTerm)
        {
            InitializeComponent();

            currentTerm = selectedTerm;


        }


        //CreateNewCoursePage is working but not appearing...

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //add the specific term info here and display to the UI

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();

                //replace "TermNumber" with ID
                var listOfCourses = conn.Query<Course>($"SELECT * FROM Course WHERE Id = '{currentTerm}'");

                listOfCourses = conn.Table<Course>().ToList();
                courseListView.ItemsSource = listOfCourses;


                //var courses = conn.Table<Course>().ToList();
                //courseListView.ItemsSource = listOfCourses;
            }

            //Not working
            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            // {
            //    conn.CreateTable<Course>();
            //    var courses = conn.Table<Course>().ToList();
            //    courseListView.ItemsSource = courses;
            //}
        }


        void CreateNewCourse_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CreateNewCoursePage(currentTerm));
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
