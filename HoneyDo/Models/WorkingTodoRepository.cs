using System.Collections.Generic;
using System.Linq;

namespace HoneyDo.Models
{
    public class WorkingTodoRepository : Repository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<Todo> GetAll()
        {
            return db.Todoes.ToList();
        }

        public Todo Find(int? id)
        {
            if (id == null)
            {
                return null;
            }
            Todo todo = db.Todoes.Find(id);
            return todo; 
        }

    }
}