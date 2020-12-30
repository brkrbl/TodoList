using Entities;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Abstract
{
    public interface ITodoManager
    {
        public  Task<List<TodoDto>> GetAllUserTodos(int userId);
        public Task DeleteTodo(int id);
        public Task<Todotable> InsertTodo(AddTodoDto todo);
        public Task<Todotable> UpdateTodo(TodoUpdateDto todo);


    }
}
