using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeCore.Dto
{
    public class ToDoListDto
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
    }
}
