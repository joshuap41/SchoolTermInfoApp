using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using SchoolTermInfoApp.Model;

namespace SchoolTermInfoApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //This method updates the page with the most recent db entry.
        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            { 
                conn.CreateTable<Term>();
                var posts = conn.Table<Term>().ToList();
            }
        }

        private void CreateNewTerm_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateNewTerm());
        }
    }
}
