using System;
using System.Collections.Generic;
using SchoolTermInfoApp.Model;
using Xamarin.Forms;
using SQLite;

namespace SchoolTermInfoApp.View
{
    public partial class EditTermPage : ContentPage
    {
        Term selectedTerm;

        public EditTermPage(Term selectedTerm)
        {
            InitializeComponent();

            this.selectedTerm = selectedTerm;

            //populates the view
            termName.Text = selectedTerm.TermName;
            startDate.Date = selectedTerm.StartDate;
            finishDate.Date = selectedTerm.FinishDate;

        }

        void UpdateButton_Clicked(System.Object sender, System.EventArgs e)
        {
            selectedTerm.TermName = termName.Text;
            selectedTerm.StartDate = startDate.Date;
            selectedTerm.FinishDate = finishDate.Date;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Term>();
                int rows = conn.Update(selectedTerm);

                if (rows > 0)
                    DisplayAlert("Success", "Term successfully updated", "Ok");
                else
                    DisplayAlert("Failure", "Term failed to update", "Ok");
            }

        }

        void DeleteButton_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Term>();
                int rows = conn.Delete(selectedTerm);

                if (rows > 0)
                    DisplayAlert("Success", "Term successfully deleted", "Ok");
                else
                    DisplayAlert("Failure", "Term failed to delete", "Ok");
            }

        }
    }
}
