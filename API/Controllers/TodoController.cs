using BL.Abstract;
using BL.Manager;
using Entities;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoManager _todoManager;
        
        public TodoController()
        {
            _todoManager = new TodoManager();
        }
        [AllowAnonymous]

        [HttpGet("{id}")]
        public async Task<List<TodoDto>> Get(int id)
        {
            return await _todoManager.GetAllUserTodos(id);
        }

        [HttpPost]
        public async Task<Todotable> Post([FromBody] AddTodoDto todo)
        {
            return await _todoManager.InsertTodo(todo);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _todoManager.DeleteTodo(id);
        }

        [HttpPut]
        public async Task<Todotable> Put([FromBody] TodoUpdateDto todo)
        {
            return await _todoManager.UpdateTodo(todo);
        }
    }
}
