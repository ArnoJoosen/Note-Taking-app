using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Server.Repositories;
using Shared.dto;

namespace Server.Controllers {

    [ApiController]
    [Route("api/todo")]
    public class TodoController : ControllerBase {

        ITodoRepo _todoRepo;
        public TodoController(ITodoRepo todoRepo) {
            _todoRepo = todoRepo;
        }

        [HttpGet]
        public ActionResult GetAllTodos() {
            List<TodoListItemReadDto> todoListItemReadDtos = new();
            var todos = _todoRepo.GetAllTodos();
            foreach (var todo in todos) {
                todoListItemReadDtos.Add(new TodoListItemReadDto {
                    Id = todo.Id,
                    Title = todo.Title,
                    Detline = todo.Detline,
                    HasDetline = todo.HasDetline,
                    IsCompleted = todo.IsCompleted
                });
            }
            return Ok(todoListItemReadDtos);
        }

        [HttpGet("not-completed")]
        public ActionResult GetNotCompletedTodos() {
            var todos = _todoRepo.GetNotCompletedTodos();
            List<TodoListItemReadDto> todoListItemReadDtos = new();
            foreach (var todo in todos) {
                todoListItemReadDtos.Add(new TodoListItemReadDto {
                    Id = todo.Id,
                    Title = todo.Title,
                    Detline = todo.Detline,
                    HasDetline = todo.HasDetline,
                    IsCompleted = todo.IsCompleted
                });
            }
            return Ok(todoListItemReadDtos);
        }

        [HttpGet("{id}")]
        public ActionResult GetTodoById(int id) {
            try {
                Todo todo = _todoRepo.GetTodoById(id);
                return Ok(new TodoReadDto {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Detline = todo.Detline,
                    HasDetline = todo.HasDetline,
                    IsCompleted = todo.IsCompleted,
                    CreatedAt = todo.CreatedAt
                });
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateTodo(TodoWriteDto wTodo) {
            Todo todo = _todoRepo.CreateTodo(wTodo.Title, wTodo.Description, wTodo.Detline, wTodo.HasDetline);
            return Ok(new TodoReadDto {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Detline = todo.Detline,
                HasDetline = todo.HasDetline,
                IsCompleted = todo.IsCompleted
            });
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTodoById(int id, TodoWriteDto uTodo) {
            try {
                Todo todo = _todoRepo.GetTodoById(id);
                todo.Title = uTodo.Title;
                todo.Description = uTodo.Description;
                todo.Detline = uTodo.Detline;
                todo.HasDetline = uTodo.HasDetline;
                todo.IsCompleted = uTodo.IsCompleted;
                _todoRepo.UpdateTodoById(todo);
                return Ok(new TodoReadDto {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Detline = todo.Detline,
                    HasDetline = todo.HasDetline,
                    IsCompleted = todo.IsCompleted
                });
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}/state")]
        public ActionResult UpdateTodoState(int id, bool isCompleted) {
            try {
                _todoRepo.UpdateTodoState(id, isCompleted);
                return Ok($"Update todo state by id {id}");
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodoById(int id) {
            try {
                _todoRepo.DeleteTodoById(id);
                return Ok($"Delete todo by id {id}");
            } catch (Exception e) {
                return NotFound(e.Message);
            }
        }
    }
}