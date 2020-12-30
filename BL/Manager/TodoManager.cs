using BL.Abstract;
using Entities;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BL.Manager
{
    public class TodoManager : ITodoManager
    {
        private readonly DAL.Abstract.ITodoRepository _todoRepository;
        private readonly DAL.Abstract.IUserRepository _userRepository;

        public TodoManager()
        {
            _todoRepository = new DAL.TodoRepository();
            _userRepository = new DAL.UserRepository();
        }

        public async Task<List<TodoDto>> GetAllUserTodos(int userId)
        {
            List<TodoDto> userTodos = new List<TodoDto>();
            foreach(var todo in await _todoRepository.GetAllTodos())
            {
                if (userId == todo.Puserid)
                {
                    var user = await _userRepository.GetUserById(userId);
                    TodoDto temp = new TodoDto(todo.Id, todo.Theader, todo.Tcontent, user, todo.Pdate);
                    userTodos.Add(temp);
                }             
            }
            return userTodos;
        }

        public async Task DeleteTodo(int id)
        {
            await _todoRepository.DeleteTodo(id);
        }

        public async Task<Todotable> InsertTodo(AddTodoDto todo)
        {
            if (todo.header == null || todo.content == null)
                return null;
            else
            {
                var newTodo = new Todotable();
                newTodo.Pdate = DateTime.Now;
                newTodo.Puserid = todo.userId;
                newTodo.Tcontent = todo.content;
                newTodo.Theader = todo.header;
                return await _todoRepository.InsertTodo(newTodo);
            }
                
        }

        public async Task<Todotable> UpdateTodo(TodoUpdateDto todo)
        {
            if ( todo.header == null || todo.content == null)
                return null;
            else
            {
                int userId = todo.userId;
                DateTime date = DateTime.UtcNow;
                Todotable utodo = new Todotable();
                utodo.Id = todo.todoId;
                utodo.Pdate = date;
                utodo.Puserid = todo.userId;
                utodo.Theader = todo.header;
                utodo.Tcontent = todo.content;
                return await _todoRepository.UpdateTodo(utodo);
            }
                
        }

    }
}
