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
                var rows = conn.Insert(createTerm);


                if (rows > 0)
                {
                    DisplayAlert("Success", "New Term Added", "OK");
                }
                else
                {
                    DisplayAlert("Failure", "Term Failed to be Added", "OK");
                }

                //Make sure that the finish date is greater than the start date
                //Check for nulls with the name and display an alert if it is bad
                //if(createTerm.StartDate < createTerm.FinishDate)

            }
            Navigation.PushAsync(new MainPage());
        }
    }
}