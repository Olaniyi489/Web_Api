using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Data.Model
{
    public class ToDoList 
    {
        [Key]
        public int Id { get; set; }
        public string Task { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now; 
    }
}
