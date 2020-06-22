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

namespace SchoolTermInfoApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public static string SelectedTerm = string.Empty;

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
                var terms = conn.Table<Term>().ToList();
                termListView.ItemsSource = terms;
            }
        }




        //used to track the selected term.
        void TermListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //casting usign the "as"
            var selectedTerm = termListView.SelectedItem as Term;

            //sets the class member
            var SelectedTerm = selectedTerm;

            if (selectedTerm != null)
            {
                Navigation.PushAsync(new TermPage(selectedTerm));
            }

        }








        private void CreateNewTerm_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateNewTerm());
        }
    }
}
