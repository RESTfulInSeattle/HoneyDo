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

        public string CssClass
        {
            //I am still marinating on how to put the this logic in the controller
            //but at least it's not in the View
            get { return Deadline.Date < DateTime.Today && !Completed ? "overdue" : "notoverdue"; }
        }
    }
}