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
        //carrying the specific term from the MainPage ItemSelected event for the EditTermPage
        Term currentTerm;
        public Term SelectedTerm { get; set; }
        //not quite sure what I was doing here...
        //private ObservableCollection<Course> listOfCourses;

        //keeping track of the course
        public static string SelectedCourse = string.Empty;

        public TermPage(Term selectedTerm)
        {
            InitializeComponent();

            //use current term to save courses to it??
            currentTerm = selectedTerm;
            this.SelectedTerm = selectedTerm;
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


        //need to keep the courses in the right term...
        void CreateNewCourse_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CreateNewCoursePage(currentTerm));
        }


        //tracking the selected course
        void CourseListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedCourse = courseListView.SelectedItem as Course;

            //sets the class member
            var SelectedCourse = selectedCourse;

            if (selectedCourse != null)
            {
                Navigation.PushAsync(new CoursePage(selectedCourse, currentTerm));
            }
        }


        
        void EditTerm_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EditTermPage(currentTerm));
        }
    }
}
