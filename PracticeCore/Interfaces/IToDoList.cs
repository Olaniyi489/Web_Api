using Practice.Data.Model;
using PracticeCore.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeCore.Interfaces
{
    public interface IToDoList
    {
        Task<bool> AddToDoList(ToDoListDto toDoListDto);

        Task<List<ToDoListDto>> GetAlltask();

        Task<ToDoListDto> GetTask(int id);

        Task UpdateTask(int id, ToDoListDto toDoListDto);
        // Task UpdateTask(int id, ToDoListDto toDoListDto);

        Task DeleteTask(int id);
    }
}
