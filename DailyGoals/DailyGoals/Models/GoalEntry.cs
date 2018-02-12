using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DailyGoals.Models
{
    public class GoalEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int GoalId { get; set; }

        public bool Completed { get; set; }

        public string Details { get; set; }

        public DateTime Date { get; set; }

    }
}
