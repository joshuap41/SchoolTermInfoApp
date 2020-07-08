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

            termName.Text = selectedTerm.TermName;
            startDate.Date = selectedTerm.StartDate;
            finishDate.Date = selectedTerm.FinishDate;
        }

        void DeleteButton_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Term>();

                var rows = conn.Delete(selectedTerm);

                if (rows > 0)
                    DisplayAlert("Success", "Term successfully deleted", "Ok");
                else
                    DisplayAlert("Failure", "Term failed to delete", "Ok");
            }
            Navigation.PushAsync(new MainPage());
        }

        private void HomeButtonToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void SaveButtonToolbarItem_Clicked(object sender, EventArgs e)
        {
            selectedTerm.TermName = termName.Text;
            selectedTerm.StartDate = startDate.Date;
            selectedTerm.FinishDate = finishDate.Date;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Term>();

                if (startDate.Date < finishDate.Date)
                {
                    if (string.IsNullOrWhiteSpace(termName.Text))
                    {
                        DisplayAlert("Failure", "Please provide all term information", "OK");
                    }
                    else
                    {
                        conn.Update(selectedTerm);
                        DisplayAlert("Success", "Term successfully updated", "Ok");
                        Navigation.PushAsync(new MainPage());
                    }
                }
                else
                {
                    DisplayAlert("Failure", "The start date cannot be after the finish date", "OK");
                }
            }
            
        }
    }
}
