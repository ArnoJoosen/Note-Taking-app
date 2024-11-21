using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using NoteTakingServer.Repositories;

namespace NoteTakingServer.Controllers {

    [ApiController]
    [Route("api/todo")]
    public class TodoController : ControllerBase {

        ITodoRepo _todoRepo;
        public TodoController(ITodoRepo todoRepo) {
            _todoRepo = todoRepo;
        }

        [HttpGet]
        public ActionResult GetAllTodos() {
            return Ok(_todoRepo.GetAllTodos());
        }

        [HttpGet("{id}")]
        public ActionResult GetTodoById(int id) {
            return Ok(_todoRepo.GetTodoById(id));
        }

        [HttpPost]
        public ActionResult CreateTodo() {
            return Ok("Create todo");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTodoById(int id) {
            return Ok($"Update todo by id {id}");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodoById(int id) {
            return Ok($"Delete todo by id {id}");
        }
    }
}