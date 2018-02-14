using DailyGoals.Droid;
using DailyGoals.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyGoals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Tomorrow : ContentPage
	{
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<GoalDetail> _goalDetails;

        public Tomorrow ()
		{
			InitializeComponent ();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            BindingContext = this;
		}

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<GoalEntry>();

            // https://stackoverflow.com/questions/27260905/join-tables-in-sqlite-net-with-linq-on-xamarin-android-is-not-supported
            var tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            var goals = await _connection.QueryAsync<GoalDetail>(
               "SELECT GoalEntry.Id, Goal.Name AS GoalName, Completed, Details, Date FROM GoalEntry " +
               "INNER JOIN Goal " +
               "ON Goal.Id = GoalEntry.GoalId " +
               "WHERE date(Date / 10000000 - 62135596800, 'unixepoch') = date('" + tomorrow + "')");

            _goalDetails = new ObservableCollection<GoalDetail>(goals);

            //var goalEntries = await _connection.Table<GoalEntry>().Where(g => g.Date == DateTime.Now.Date).ToListAsync();
            //_goalEntries = new ObservableCollection<GoalEntry>(goalEntries);

            goalEntriesListView.ItemsSource = _goalDetails;

            base.OnAppearing();
        }

        public string CurrentDate
        {
            get
            {
                return DateTime.Now.AddDays(1).ToShortDateString();
            }
        }
    }
}