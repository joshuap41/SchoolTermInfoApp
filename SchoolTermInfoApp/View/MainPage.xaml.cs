using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;
using Plugin.LocalNotifications;

namespace SchoolTermInfoApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]


    public partial class MainPage : ContentPage
    {
        private bool _firstAppearnce = true;

        //public static string SelectedTerm = string.Empty;

        public MainPage()
        {
            InitializeComponent();
        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.MyTermInformation();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            { 
                conn.CreateTable<Term>();
                var terms = conn.Table<Term>().ToList();
                termListView.ItemsSource = terms;

                conn.CreateTable<Course>();
                var courseList = conn.Table<Course>().ToList();

                conn.CreateTable<Assessment>();
                var assessmentList = conn.Table<Assessment>().ToList();



            //This needs refactoring a lot!
                if (_firstAppearnce)
                {
                    _firstAppearnce = false;

                    int courseId = 0;
                    foreach (Course course in courseList)
                    {
                        courseId++;
                        if (course.CourseNotifications == 1)
                        {
                            if (course.StartDate == DateTime.Today)
                                CrossLocalNotifications.Current.Show("Reminder", $"{course.CourseName} starts today!", courseId);
                            if (course.FinishDate == DateTime.Today)
                                CrossLocalNotifications.Current.Show("Reminder", $"{course.CourseName} ends today!", courseId);
                        }
                    }


                    int assessmentId = courseId;
                    foreach (Assessment assessment in assessmentList)
                    {
                        assessmentId++;
                        if (assessment.AssessmentNotifications == 1)
                        {
                            if (assessment.StartDate == DateTime.Today)
                                CrossLocalNotifications.Current.Show("Reminder", $"{assessment.AssessmentName} starts today!", assessmentId);
                            if (assessment.FinishDate == DateTime.Today)
                                CrossLocalNotifications.Current.Show("Reminder", $"{assessment.AssessmentName} ends today!", assessmentId);
                        }
                    }
                }
            }
        }


        //used to track the selected term.
        void TermListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //casting usign the "as"
            var selectedTerm = termListView.SelectedItem as Term;

            if (selectedTerm != null)
                Navigation.PushAsync(new TermPage(selectedTerm));
        }



        private void CreateNewTerm_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateNewTerm());
        }
    }
}
