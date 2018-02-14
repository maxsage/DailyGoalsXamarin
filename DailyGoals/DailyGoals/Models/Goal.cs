using System;

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyGoals.Models
{
    public class Goal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Current { get; set; }

    }
}
