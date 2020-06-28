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
        public static string dateFormat = " MM/dd/yyyy";

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


        public static int CourseCountCheck(Term selectedTerm)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();

                var courseTable = conn.Table<Course>().ToList();

                //gets courses for each term
                var listOfCourses = (from course in courseTable
                                     where course.TermNumber == selectedTerm.Id
                                     select course).ToList();

                var count = listOfCourses.Count();

                return count;
            }

        }

        public static int ObjectiveAssessmentCountCheck(Course selectedCourse)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Assessment>();

                var assessmentTable = conn.Table<Assessment>().ToList();



            //need amount of each assessmentType. Each course can only contain 1 of each assessmentType

            //https://stackoverflow.com/questions/8791540/multiple-where-clauses-with-linq-extension-methods
            //Working on the "Where" query
                var listOfAssessments = (from Assessment in assessmentTable
                                         where Assessment.CourseNumber ==selectedCourse.Id
                                         where Assessment.AssessmentType == "Objective Assessment"
                                         select Assessment).ToList();

                var objectiveAssessmentCount = listOfAssessments.Count();

                



                return objectiveAssessmentCount;
            }

            
        }

    }
}
