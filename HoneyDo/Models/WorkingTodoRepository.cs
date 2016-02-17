using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoneyDo.Models
{
    public class WorkingTodoRepository : Repository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<Todo> GetAll()
        {
            return db.Todoes.ToList();
        }

    }
}