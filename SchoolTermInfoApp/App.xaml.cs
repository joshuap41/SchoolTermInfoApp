using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;
using System.Linq;
using Plugin.LocalNotifications;
using System.Text.RegularExpressions;
using System.Globalization;


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

            //count of Objective Assessments for a specific course
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

            //count of Performance Assessments for a specific course
                var listOfAssessments = (from Assessment in assessmentTable
                                         where Assessment.CourseNumber == selectedCourse.Id
                                         where Assessment.AssessmentType == "Performance Assessment"
                                         select Assessment).ToList();

                var performanceAssessmentCount = listOfAssessments.Count();

                return performanceAssessmentCount;
            }
        }

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
                        FinishDate = new DateTime(2020, 07, 07)
                    };
                    conn.Insert(myTerm);

                    Course myCourse = new Course()
                    {
                        TermNumber = myTerm.Id,
                        CourseName = "Mobile App Development",
                        MentorName = "Joshua Johnson",
                        MentorPhoneNumber = "1(555)555-5555",
                        MentorEmail = "jjoh706@wgu.edu",
                        CourseStatus = "Active",
                        StartDate = new DateTime(2020, 07, 01),
                        FinishDate = new DateTime(2020, 07, 07),
                        CourseNotes = "This class is awesome!",
                        CourseNotifications = 1,
                    };
                    conn.Insert(myCourse);

                    Assessment myObAssessment = new Assessment()
                    {
                        AssessmentName = "Ob Assessment 1",
                        CourseNumber = myCourse.Id,
                        StartDate = new DateTime(2020, 07, 01),
                        FinishDate = new DateTime(2020, 07, 07),
                        AssessmentType = "Objective Assessment",
                        AssessmentNotifications = 1
                    };
                    conn.Insert(myObAssessment);

                    Assessment myPerAssessment = new Assessment()
                    {
                        AssessmentName = "Per Assessment 2",
                        CourseNumber = myCourse.Id,
                        StartDate = new DateTime(2020, 07, 01),
                        FinishDate = new DateTime(2020, 07, 07),
                        AssessmentType = "Performance Assessment",
                        AssessmentNotifications = 1
                    };
                    conn.Insert(myPerAssessment);
                }
            }
        }

        //https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        public static bool IsValidEmail(string mentorEmail)
        {
            if (string.IsNullOrWhiteSpace(mentorEmail))
            {
                return false;
            }
                
            try
            {
                // Normalize the domain
                mentorEmail = Regex.Replace(mentorEmail, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(mentorEmail,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
