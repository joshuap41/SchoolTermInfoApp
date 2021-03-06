﻿using System;
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
        private Term selectedTerm;

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
                conn.CreateTable<Course>();
                var courseTable = conn.Table<Course>().ToList();

                //queries the list of courses and displays for the selectedTerm
                var listOfCourses = (from course in courseTable
                                     where course.TermNumber == selectedTerm.Id
                                     select course).ToList();

                courseListView.ItemsSource = listOfCourses;
            }
        }


        //Need to fix back button to not return to the "CreateNewCourse" page and add more courses
        void CreateNewCourse_Clicked(System.Object sender, System.EventArgs e)
        {
            int count = App.CourseCountCheck(selectedTerm);

            if (count <= 5)
                Navigation.PushAsync(new CreateNewCoursePage(selectedTerm));
            else
                DisplayAlert("Failure", "Only 6 Courses Allowed Per Term", "OK");
        }


        void CourseListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedCourse = courseListView.SelectedItem as Course;

            if (selectedCourse != null)
                Navigation.PushAsync(new CoursePage(selectedCourse, selectedTerm));
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
