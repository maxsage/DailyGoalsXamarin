using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DailyGoals.Droid;
using SQLite;
using System.Collections.ObjectModel;
using DailyGoals.Models;

namespace DailyGoals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Today : ContentPage
	{
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<GoalDetail> _goalDetails;

        public Today ()
		{
			InitializeComponent ();

            var ticks = DateTime.Now.Ticks;

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            BindingContext = this;
		}

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<GoalEntry>();

            // https://stackoverflow.com/questions/27260905/join-tables-in-sqlite-net-with-linq-on-xamarin-android-is-not-supported
            var goals = await _connection.QueryAsync<GoalDetail>(
                "SELECT GoalEntry.Id, Goal.Name AS GoalName, Completed, Details, Date FROM GoalEntry " +
                "INNER JOIN Goal " + 
                "ON Goal.Id = GoalEntry.GoalId " +
                "WHERE date(Date / 10000000 - 62135596800, 'unixepoch') = date('now')");
            _goalDetails = new ObservableCollection<GoalDetail>(goals);
            
            //var goalEntries = await _connection.Table<GoalEntry>().Where(g => g.Date == DateTime.Now.Date).ToListAsync();
            //_goalEntries = new ObservableCollection<GoalEntry>(goalEntries);

            goalEntriesListView.ItemsSource = _goalDetails;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            // Should probably save to database here.

            base.OnDisappearing();
        }

        //private void Completed_Toggled(object sender, ValueChangedEventArgs e)
        //{
        //    //DisplayAlert("Toggling", "Toggled", "Ok");
        //    var goal = sender;
        //}

        private void goalEntriesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var goalDetail = e.Item as GoalDetail;
            goalDetail.Completed = !goalDetail.Completed;

            //goalDetail.GoalColor = goalDetail.Completed ? Color.LightGreen : Color.LightSalmon;

            //DisplayAlert("Tapped", goalDetail.GoalName, "OK");
        }

        private void goalEntriesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            goalEntriesListView.SelectedItem = null;
            //var goalDetail = e.SelectedItem as GoalDetail;
            //DisplayAlert("Selected", goalDetail.GoalName, "OK");
        }


        public string CurrentDate
        {
            get
            {
                return DateTime.Now.ToShortDateString();
            }
        }
    }
}