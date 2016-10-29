using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak1.Interfaces;
using Zadatak1.Models;

namespace Zadatak1.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository(IGenericList<TodoItem> initialDBstate = null)
        {
            _inMemoryTodoDatabase = initialDBstate ?? new GenericList<TodoItem>();
        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }
            TodoItem i = _inMemoryTodoDatabase.Where(x => x.Id == todoItem.Id).FirstOrDefault();
            if (i != null)
            {
                throw new DuplicateTodoItemException("duplicate id: {" + i.Id + "}");
            }
            _inMemoryTodoDatabase.Add(todoItem);
        }

        public TodoItem Get(Guid todoId)
        {
            return _inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault();
        }

        public List<TodoItem> GetActive()
        {
            return _inMemoryTodoDatabase.Where(i => i.IsCompleted == false).ToList();
        }

        public List<TodoItem> GetAll()
        {
            return _inMemoryTodoDatabase.OrderByDescending(i => i.DateCreated).ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            return _inMemoryTodoDatabase.Where(i => i.IsCompleted).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return _inMemoryTodoDatabase.Where(i => filterFunction(i)).ToList();
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            TodoItem item = _inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault();
            if (item == null)
            {
                return false;
            }
            item.IsCompleted = true;
            return true;
        }

        public bool Remove(Guid todoId)
        {
            TodoItem item = _inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault();
            if (item == null)
            {
                return false;
            }
            _inMemoryTodoDatabase.Remove(item);
            return true;
        }

        public void Update(TodoItem todoItem)
        {
            TodoItem item = _inMemoryTodoDatabase.Where(i => i.Id == todoItem.Id).FirstOrDefault();
            if (item == null)
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
            item = todoItem;
        }

        



    }
}
