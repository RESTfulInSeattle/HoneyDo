using System.Collections.Generic;

namespace HoneyDo.Models
{
    public interface Repository
    {
        List<Todo> GetAll();
    }
}