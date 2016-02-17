using System;
using System.ComponentModel.DataAnnotations;

namespace HoneyDo.Models
{
    public class Todo
    {
        public int TodoId { get; set; }
        public int OwnerId { get; set; }
        public  string TaskName { get; set; }
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        public bool Completed { get; set; }

        public string Moredetails { get; set; }
    }
}