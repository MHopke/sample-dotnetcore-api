using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using sampledotnetcoreapi.Models;

namespace sample_dotnetcore_api.Controllers
{
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        #region Private Vars
        readonly ToDoContext _toDoContext;
        #endregion

        #region Constructors
        public ToDoController(ToDoContext context)
        {
            _toDoContext = context;

            if (_toDoContext.ToDoItems.Count() == 0)
            {
                _toDoContext.ToDoItems.Add(new ToDoItem { Name = "Item1" });
                _toDoContext.SaveChanges();
            }
        }
        #endregion

        #region GET Methods
        [HttpGet]
        public IEnumerable<ToDoItem> GetAll()
        {
            return _toDoContext.ToDoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetToDo")]
        public IActionResult GetToDo(long id)
        {
            var item = _toDoContext.ToDoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        #endregion

        #region POST Methods
        [HttpPost]
        public IActionResult CreateToDo([FromBody] ToDoItem item)
        {
            if (item == null)
                return BadRequest();

            _toDoContext.ToDoItems.Add(item);
            _toDoContext.SaveChanges();

            return CreatedAtRoute("GetToDo", new { id = item.Id }, item);
        }
        #endregion

        #region PUT Methods
        [HttpPut]
        public IActionResult UpdateToDo(long id, [FromBody] ToDoItem item)
        {
            if (item == null)
                return BadRequest();

            ToDoItem dbItem = _toDoContext.ToDoItems.FirstOrDefault<ToDoItem>(x => x.Id == id);
            if (dbItem == null)
                return NotFound();

            dbItem.IsComplete = item.IsComplete;
            dbItem.Name = item.Name;

            _toDoContext.ToDoItems.Update(dbItem);
            _toDoContext.SaveChanges();

            return new NoContentResult();
        }
        #endregion

        #region DELETE Methods
        [HttpDelete]
        public IActionResult DeleteToDo(long id)
        {
            ToDoItem dbItem = _toDoContext.ToDoItems.FirstOrDefault<ToDoItem>(x => x.Id == id);
            if (dbItem == null)
                return NotFound();

            _toDoContext.ToDoItems.Remove(dbItem);
            _toDoContext.SaveChanges();

            return new NoContentResult();
        }
        #endregion
    }
}
