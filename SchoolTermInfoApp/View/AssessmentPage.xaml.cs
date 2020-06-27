using System;
using System.Collections.Generic;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;


using Xamarin.Forms;

namespace SchoolTermInfoApp.View
{
    public partial class AssessmentPage : ContentPage
    {
        private Course selectedCourse;

        public AssessmentPage(Course selectedCourse)
        {
            InitializeComponent();

            this.selectedCourse = selectedCourse;
        }
    }
}
