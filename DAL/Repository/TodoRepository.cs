using System.Collections.Generic;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DAL.Abstract;

namespace DAL
{
    public class TodoRepository : ITodoRepository
    {
        public async Task<List<Todotable>> GetAllTodos()
        {
            using (var db = new DatabaseContext())
            {
                return await db.Todotables.ToListAsync();
            }
        }

        public async Task<Todotable> GetTodoById(int id)
        {
            using(var db = new DatabaseContext())
            {
                return await db.Todotables.FindAsync(id);
            }
        }

        public async Task DeleteTodo(int id)
        {
            using (var db = new DatabaseContext())
            {

                var deleteTodo = await GetTodoById(id);
                db.Todotables.Remove(deleteTodo);
                await db.SaveChangesAsync();

            }
        }
        public async Task<Todotable> InsertTodo(Todotable todo)
        {
            using (var db = new DatabaseContext())
            {
                db.Todotables.Add(todo);
                await db.SaveChangesAsync();
                return todo;
            }
        }
        public async Task<Todotable> UpdateTodo(Todotable todo)
        {
            using (var db = new DatabaseContext())
            {
                db.Todotables.Update(todo);
                await db.SaveChangesAsync();
                return todo;
            }
        }
    }
}
