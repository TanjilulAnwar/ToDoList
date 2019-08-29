using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class Done
    {
        public int id { get; set; }
        public ApplicationUser User { get; set; }
        public string Description { get; set; }
      
    }
}