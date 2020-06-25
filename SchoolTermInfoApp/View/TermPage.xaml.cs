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
        Term selectedTerm;

       // private ObservableCollection<Course> _listOfCourses;

        public TermPage(Term selectedTerm)
        {
            InitializeComponent();

            this.selectedTerm = selectedTerm;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            //add the specific term info here and display to the UI

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                 conn.CreateTable<Course>();

                //not getting the course that was just created...n Count 0 here

                //var query = $"SELECT * FROM Course WHERE Id = '{selectedTerm.Id}'";
                //var listOfCourses = conn.Query<Course>(query);
                //var rows = conn.Table<Course>().ToList();

                //courseListView.ItemsSource = listOfCourses;
                //not able to use await
                //_listOfCourses = new ObservableCollection<Course>(listOfCourses);
                //courseListView.ItemsSource = _listOfCourses;

                //displays all courses in every term
                var course = conn.Table<Course>().ToList();
                courseListView.ItemsSource = course;







            }
        }


        //need to keep the courses in the right term...
        void CreateNewCourse_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CreateNewCoursePage(selectedTerm));
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
