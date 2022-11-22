using Practice.Data;
using Practice.Data.Model;
using PracticeCore.Dto;
using PracticeCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeCore.Services
{
    public class ToDoListService : IToDoList
    {
        private PracticeIdentityDbContext _db;
        public ToDoListService(PracticeIdentityDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddToDoList(ToDoListDto toDoListDto)
        {
            var newTask = new ToDoList () { Task = toDoListDto.Task, Category = toDoListDto.Category, Priority =toDoListDto?.Priority, dateTime = toDoListDto.dateTime };
            _db.toDoLists.Add(newTask);
            var result = await _db.SaveChangesAsync();
            return result > 0 ? true : false;
        }

    }
}
