using Microsoft.AspNetCore.Mvc;
using PracticeCore.Dto;
using PracticeCore.Interfaces;
using PracticeCore.Services;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography.X509Certificates;
using Practice.Data.Model;

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

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var users = await _toDoList.GetAlltask();
            if (users == null)
            {
                return NotFound("No Task Added to List");
            }

            return Ok(users);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAList(int id)
        {
            try
            {
                var users = await _toDoList.GetTask(id);
                if (users == null)
                {
                    return StatusCode(404, new { Message = "User not found !" });
                }

                return StatusCode(200, users);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList([FromBody] ToDoListDto toDoListDto, [FromRoute] int id)
        {
            await _toDoList.UpdateTask(id, toDoListDto);

            return StatusCode(201, new { message = "Task Updated Successfully !!" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdateList([FromRoute] int id)
        {
            await _toDoList.DeleteTask(id);

            return StatusCode(201, new { message = "Task Deleted Successfully !!" }); 
        }
    }
}
