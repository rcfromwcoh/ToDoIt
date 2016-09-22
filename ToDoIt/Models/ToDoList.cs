using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoIt.Models
{
    public class ToDoList
    {
        [Key]
        public int ToDoListID { get; set; }

        public string Title { get; set; }

        public virtual ICollection<ToDoItem> ToDoItems { get; set; }
    }
}