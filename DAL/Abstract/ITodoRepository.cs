using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ITodoRepository
    {
        public Task<List<Todotable>> GetAllTodos();
        public Task<Todotable> GetTodoById(int id);
        public Task DeleteTodo(int id);
        public Task<Todotable> InsertTodo(Todotable todo);
        public Task<Todotable> UpdateTodo(Todotable todo);

    }
}
