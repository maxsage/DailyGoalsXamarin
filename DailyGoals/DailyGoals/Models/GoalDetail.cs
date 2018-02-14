using System;
using System.Collections.Generic;
using System.Text;

namespace DailyGoals.Models
{
    public class GoalDetail
    {
        public int Id { get; set; }

        public string GoalName { get; set; }

        public bool Completed { get; set; }

        public string Details { get; set; }

        public DateTime Date { get; set; }
    }
}
