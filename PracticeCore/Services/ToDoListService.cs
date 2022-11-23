using Microsoft.EntityFrameworkCore;
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

        public static List<ToDoListDto> Users = new List<ToDoListDto>();
        public async Task<bool> AddToDoList(ToDoListDto toDoListDto)
        {
            var newTask = new ToDoList () { Task = toDoListDto.Task, Category = toDoListDto.Category, Priority =toDoListDto?.Priority, dateTime = toDoListDto.dateTime };
            _db.toDoLists.Add(newTask);
            var result = await _db.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<List<ToDoListDto>> GetAlltask()
        {
            //var result = await _db.toDoLists.ToListAsync();

            //return result;

            return _db.toDoLists.Select(x => new ToDoListDto()
            {
                Task = x.Task,
                Category = x.Category,
                Priority = x.Priority,
                dateTime = x.dateTime
            }).ToList();
        }

        public async Task<ToDoListDto> GetTask(int id)
        {
            var task = await _db.toDoLists.Where( x => x.Id == id).Select(x => new ToDoListDto() 
            {
                Category = x.Category,
                Priority=x.Priority,
                dateTime=x.dateTime,
                Task = x.Task
            }).FirstOrDefaultAsync();
               return task;
        }


        public async Task UpdateTask(int id, ToDoListDto toDoListDto)
        {
            var list = new ToDoList()
            {
                Id = id,
                Task = toDoListDto.Task,
                Category = toDoListDto.Category,
                Priority = toDoListDto.Priority,
                dateTime = toDoListDto.dateTime
            };
             _db.toDoLists.Update(list);
           await _db.SaveChangesAsync();

        }

        public async Task DeleteTask(int id)
        {
            var task = new ToDoList() { Id = id };
            
            _db.toDoLists.Remove(task);

            await _db.SaveChangesAsync();
        }
    }
}
