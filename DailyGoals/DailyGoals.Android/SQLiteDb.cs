using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using DailyGoals.Droid;
using DailyGoals;

[assembly: Dependency(typeof(SQLiteDb))]

namespace DailyGoals.Droid
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            //var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "DailyGoals.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}