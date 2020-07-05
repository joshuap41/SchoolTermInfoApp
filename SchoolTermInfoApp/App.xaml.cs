using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;
using System.Linq;
using Plugin.LocalNotifications;


namespace SchoolTermInfoApp
{
    public partial class App : Application
    {
        public static string dateFormat = " MM/dd/yyyy";

        public static string DatabaseLocation = string.Empty;
        
        public App()
        {
            InitializeComponent();

            //NavigationPage creates a back button for use in iOS
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

            //need count of Objective Assessments for a specific course

                var listOfAssessments = (from Assessment in assessmentTable
                                         where Assessment.CourseNumber ==selectedCourse.Id
                                         where Assessment.AssessmentType == "Objective Assessment"
                                         select Assessment).ToList();

                var objectiveAssessmentCount = listOfAssessments.Count();

                return objectiveAssessmentCount;
            }

            
        }

        public static int PerformanceAssessmentCountCheck(Course selectedCourse)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Assessment>();

                var assessmentTable = conn.Table<Assessment>().ToList();

                //need count of Performance Assessments for a specific course

                var listOfAssessments = (from Assessment in assessmentTable
                                         where Assessment.CourseNumber == selectedCourse.Id
                                         where Assessment.AssessmentType == "Performance Assessment"
                                         select Assessment).ToList();

                var performanceAssessmentCount = listOfAssessments.Count();

                return performanceAssessmentCount;
            }


        }
        //PerformanceAssessmentCheck()???

        public static void MyTermInformation()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Term>();
                var termTable = conn.Table<Term>().ToList();

                var listOfTerms = (from term in termTable
                                     where term.TermName == "Final Term"
                                     select term).Distinct().ToList();

                var lists = (from term in termTable
                             select term.TermName == "Final Term").ToList();

                conn.CreateTable<Course>();
                conn.CreateTable<Assessment>();

                if(termTable.Any() == false)
                {
                    Term myTerm = new Term()
                    {
                        TermName = "Final Term",
                        StartDate = new DateTime(2020, 07, 01),
                        FinishDate = new DateTime(2020, 07, 05)
                    };
                    conn.Insert(myTerm);

                    Course myCourse = new Course()
                    {
                        TermNumber = myTerm.Id,
                        CourseName = "Mobile App Development",
                        MentorName = "Joshua Johnson",
                        MentorPhoneNumber = "1(706)572-6816",
                        MentorEmail = "jjoh706@wgu.edu",
                        CourseStatus = "Active",
                        StartDate = new DateTime(2020, 07, 01),
                        FinishDate = new DateTime(2020, 07, 05),
                        CourseNotes = "This class is awesome!",
                        CourseNotifications = 1,
                    };
                    conn.Insert(myCourse);

                    Assessment myObAssessment = new Assessment()
                    {
                        AssessmentName = "Ob Assessment 1",
                        CourseNumber = myCourse.Id,
                        StartDate = new DateTime(2020, 07, 01),
                        FinishDate = new DateTime(2020, 07, 05),
                        AssessmentType = "Objective Assessment",
                        AssessmentNotifications = 1
                    };
                    conn.Insert(myObAssessment);

                    Assessment myPerAssessment = new Assessment()
                    {
                        AssessmentName = "Per Assessment 2",
                        CourseNumber = myCourse.Id,
                        StartDate = new DateTime(2020, 07, 01),
                        FinishDate = new DateTime(2020, 07, 05),
                        AssessmentType = "Performance Assessment",
                        AssessmentNotifications = 1
                    };
                    conn.Insert(myPerAssessment);
                }

            }
        }
    }
}
