using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolTermInfoApp.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SchoolTermInfoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNewTerm : ContentPage
    {
        public CreateNewTerm()
        {
            InitializeComponent();
        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Term createTerm = new Term()
            {
                TermName = termName.Text,
                StartDate = startDate.Date,
                FinishDate = finishDate.Date
            };

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
                        conn.Insert(createTerm);
                        DisplayAlert("Success", "Term successfully added", "OK");
                        Navigation.PushAsync(new MainPage());
                    }
                }
                else
                {
                    DisplayAlert("Failure", "The start date cannot be after the finish date", "OK");
                }
            }
        }

        void HomeButtonToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}