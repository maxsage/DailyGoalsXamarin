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
            var goals = await _connection.QueryAsync<GoalDetail>("SELECT GoalEntry.Id, Goal.Name AS GoalName, Completed, Details, Date FROM GoalEntry inner join Goal on Goal.Id = GoalEntry.GoalId");
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
                return DateTime.Now.ToShortDateString();
            }
        }
    }
}