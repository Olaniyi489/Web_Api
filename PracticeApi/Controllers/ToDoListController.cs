using Microsoft.AspNetCore.Mvc;
using PracticeCore.Dto;
using PracticeCore.Interfaces;
using PracticeCore.Services;
using System.Threading.Tasks;
using System;

namespace PracticeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : Controller
    {
        private IToDoList _toDoList;

        public ToDoListController(IToDoList toDoList)
        {
            _toDoList = toDoList;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost("AddToDoList")]
        public async Task<IActionResult> AddToDoList([FromBody] ToDoListDto toDoListDto)
        {

            try
            {
                //throw new Exception("Server Error");
                var result = await _toDoList.AddToDoList(toDoListDto);

                if (!result)
                {
                    return BadRequest("Unable To Add Task To ToDoList !");
                }

                return StatusCode(201, new { message = "Task Added Successfully !!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}" });

            }

        }
    }
}
