using System;
using System.Collections.Generic;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;



namespace SchoolTermInfoApp.View
{
    public partial class TermPage : ContentPage
    {
        Term selectedTerm;

       //private ObservableCollection<Course> _listOfCourses;

        public TermPage(Term selectedTerm)
        {
            InitializeComponent();

            this.selectedTerm = selectedTerm;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //displays all courses in every term
                conn.CreateTable<Course>();
                var course = conn.Table<Course>().ToList();

                //use this count to regulate only 6 courses
                var count = course.Count();

                courseListView.ItemsSource = course;


                //need to use linq to do this
                //var courseTable = conn.Table<Course>().ToList();

                //var listOfCourses = (from c in courseTable
                //                     where c.);




                //***not getting the course that was just created...n Count 0 here***
                //var query = $"SELECT * FROM Course WHERE Id = '{selectedTerm.Id}'";
                //var listOfCourses = conn.Query<Course>(query);
                //var rows = conn.Table<Course>().ToList();
                //courseListView.ItemsSource = listOfCourses;


                //not able to use await? Need to setup the db different and change all.
                //var query = $"SELECT * FROM Course WHERE Id = '{selectedTerm.Id}'";
                //var listOfCourses = await conn.Query<Course>(query);

                //_listOfCourses = new ObservableCollection<Course>(listOfCourses);
                //courseListView.ItemsSource = _listOfCourses;




            }
        }


        
        void CreateNewCourse_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();
                var course = conn.Table<Course>().ToList();
                var count = course.Count();

                if (count <= 6)
                    Navigation.PushAsync(new CreateNewCoursePage(selectedTerm));
                else
                    DisplayAlert("Error", "Only 6 courses are allowed in a single Term", "Ok");
            } 
        }


        void CourseListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedCourse = courseListView.SelectedItem as Course;


            if (selectedCourse != null)
            {
                Navigation.PushAsync(new CoursePage(selectedCourse, selectedTerm));
            }
        }


        
        void EditTerm_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EditTermPage(selectedTerm));
        }

        private void HomeButtonToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
