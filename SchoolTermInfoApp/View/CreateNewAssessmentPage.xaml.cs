using System;
using System.Collections.Generic;
using SQLite;
using SchoolTermInfoApp.Model;

using Xamarin.Forms;

namespace SchoolTermInfoApp.View
{
    public partial class CreateNewAssessmentPage : ContentPage
    {
        private Course selectedCourse;
        public CreateNewAssessmentPage(Course selectedCourse)
        {
            InitializeComponent();
            this.selectedCourse = selectedCourse;
        }

        void SaveButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
