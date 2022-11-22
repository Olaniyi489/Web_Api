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
    }
}
