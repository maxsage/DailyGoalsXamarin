using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyGoals.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
