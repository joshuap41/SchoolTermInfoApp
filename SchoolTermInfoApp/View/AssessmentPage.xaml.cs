﻿using System;
using System.Collections.Generic;
using SQLite;
using SchoolTermInfoApp.Model;
using SchoolTermInfoApp.View;


using Xamarin.Forms;

namespace SchoolTermInfoApp.View
{
    public partial class AssessmentPage : ContentPage
    {
        private Assessment selectedAssessment;
        private Course selectedCourse;

        public AssessmentPage(Assessment selectedAssessment, Course selectedCourse)
        {
            InitializeComponent();

            this.selectedAssessment = selectedAssessment;
            this.selectedCourse = selectedCourse;
        }


    }
}