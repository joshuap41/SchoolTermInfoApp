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
        

        //public static string SelectedTerm = string.Empty;

        public MainPage()
        {
            InitializeComponent();
        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.MyTermInformation();

            bool appOpening = true;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Term>();
                var terms = conn.Table<Term>().ToList();
                termListView.ItemsSource = terms;

                conn.CreateTable<Course>();
                var courseList = conn.Table<Course>().ToList();

                conn.CreateTable<Assessment>();
                var assessmentList = conn.Table<Assessment>().ToList();

                //Notifications
                try
                {
                    if (appOpening)
                    {
                        appOpening = false;

                        var courseId = 0;
                        foreach (Course course in courseList)
                        {
                            courseId++;

                            if (course.CourseNotifications == 1)
                            {
                                if (course.StartDate == DateTime.Today)
                                    CrossLocalNotifications.Current.Show("Alert", $"{course.CourseName} begins today.", courseId);
                                if (course.FinishDate == DateTime.Today)
                                    CrossLocalNotifications.Current.Show("Alert", $"{course.CourseName} finishes today.", courseId);
                            }
                        }

                        var assessmentId = courseId;
                        foreach (Assessment assessment in assessmentList)
                        {
                            assessmentId++;

                            if (assessment.AssessmentNotifications == 1)
                            {
                                if (assessment.StartDate == DateTime.Today)
                                    CrossLocalNotifications.Current.Show("Alert", $"{assessment.AssessmentName} begins today.", assessmentId);
                                if (assessment.FinishDate == DateTime.Today)
                                    CrossLocalNotifications.Current.Show("Alert", $"{assessment.AssessmentName} finishes today.", assessmentId);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    DisplayAlert("Failure", "Notifications failed to be displayed", "Ok");
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
