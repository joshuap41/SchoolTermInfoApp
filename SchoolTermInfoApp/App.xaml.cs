using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;
using System.Linq;


namespace SchoolTermInfoApp
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();

            //Creates a navigation for use in iOS
            MainPage = new NavigationPage(new MainPage());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            DatabaseLocation = databaseLocation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


        public static int CountCheck(Term selectedTerm)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();

                var courseTable = conn.Table<Course>().ToList();

                var listOfCourses = (from course in courseTable
                                     where course.TermNumber == selectedTerm.Id
                                     select course).ToList();

                var count = listOfCourses.Count();

                return count;
            }

        }

    }
}
