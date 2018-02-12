using SQLite;

namespace DailyGoals.Droid
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
