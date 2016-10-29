using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadatak1.Interfaces;
using Zadatak1.Repositories;
using Zadatak1.Models;
using System.Linq;
using System.Collections.Generic;

namespace Zadatak2
{
    [TestClass]
    public class ToDoRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);
        }

        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }

        [TestMethod]
        public void GettingNonExistingItemProducesNull()
        {
            ITodoRepository repository = new TodoRepository();
            var notAdded = new TodoItem("nothing");
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            Assert.IsTrue(repository.Get(notAdded.Id) == null);
        }

        [TestMethod]
        public void RemovingItemRemovesItem()
        {
            ITodoRepository repository = new TodoRepository();
            var removed = new TodoItem("nothing");
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            repository.Add(removed);
            Assert.IsTrue(repository.Get(removed.Id) != null);
            Assert.IsTrue(repository.Remove(removed.Id) == true);
            Assert.IsTrue(repository.Remove(removed.Id) == false);
            Assert.IsTrue(repository.Get(removed.Id) == null);
        }

        [TestMethod]
        public void UpdatingItemUpdatesItem()
        {
            ITodoRepository repository = new TodoRepository();
            var item = new TodoItem("nothing");
            repository.Update(item);
            Assert.IsTrue(repository.Get(item.Id) != null);
            Assert.IsFalse(repository.Get(item.Id).Text.Equals("something"));
            item.Text = "something";
            repository.Update(item);
            Assert.IsTrue(repository.Get(item.Id).Text.Equals("something"));
        }

        [TestMethod]
        public void MarkingAsCompletedMarksCorrectly()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            Assert.IsFalse(repository.Get(todoItem.Id).IsCompleted);
            repository.MarkAsCompleted(todoItem.Id);
            Assert.IsTrue(repository.Get(todoItem.Id).IsCompleted);
        }

        [TestMethod]
        public void GettingAllSortsByDescendingDate()
        {
            IGenericList<TodoItem> list = new GenericList<TodoItem>();

            TodoItem item2016 = new TodoItem("2016");
            TodoItem item2017 = new TodoItem("2017");
            TodoItem item2018 = new TodoItem("2018");

            item2017.DateCreated = item2017.DateCreated.AddYears(1);
            item2018.DateCreated = item2018.DateCreated.AddYears(2);

            list.Add(item2016); list.Add(item2017); list.Add(item2018);

            ITodoRepository repository = new TodoRepository(list);

            List<TodoItem> sorted = list.OrderByDescending(i => i.DateCreated).ToList();
            List<TodoItem> sortedRepo = repository.GetAll();
            Assert.IsTrue(sorted.SequenceEqual(sortedRepo));
            
        }

        [TestMethod]
        public void GetActiveGetsUncompletedItems()
        {
            List<TodoItem> list = new List<TodoItem>();
            ITodoRepository repository = new TodoRepository();

            TodoItem item2016 = new TodoItem("2016");
            TodoItem item2017 = new TodoItem("2017");
            TodoItem item2018 = new TodoItem("2018");

            item2016.MarkAsCompleted();
            item2017.MarkAsCompleted();


            list.Add(item2018);
            repository.Add(item2016); repository.Add(item2017); repository.Add(item2018);

            List<TodoItem> active = repository.GetActive();
            Assert.IsTrue(list.SequenceEqual(active));
        }

        [TestMethod]
        public void GetCompletedGetsCompletedItems()
        {
            List<TodoItem> list = new List<TodoItem>();
            ITodoRepository repository = new TodoRepository();

            TodoItem item2016 = new TodoItem("2016");
            TodoItem item2017 = new TodoItem("2017");
            TodoItem item2018 = new TodoItem("2018");
            
            item2018.MarkAsCompleted();


            list.Add(item2018);
            repository.Add(item2016); repository.Add(item2017); repository.Add(item2018);

            List<TodoItem> completed = repository.GetCompleted();
            Assert.IsTrue(list.SequenceEqual(completed));
        }

        [TestMethod]
        public void GetFilteredFiltersItems()
        {
            List<TodoItem> list = new List<TodoItem>();
            ITodoRepository repository = new TodoRepository();

            TodoItem item2016 = new TodoItem("2016");
            TodoItem item2017 = new TodoItem("2017");
            TodoItem item3018 = new TodoItem("3018");


            list.Add(item3018);
            repository.Add(item2016); repository.Add(item2017); repository.Add(item3018);

            List<TodoItem> filtered = repository.GetFiltered(x => x.Text.StartsWith("3"));
            Assert.IsTrue(list.SequenceEqual(filtered));
        }


    }
}
