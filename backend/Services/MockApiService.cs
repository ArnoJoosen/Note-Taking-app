using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.dto;
using Shared.Models;

namespace Backend.Services
{
    public class MockApiService : IApiService {
        List<Todo> _todos = new();
        List<Node> _nodes = new();
        int currentTodoId = 0;
        int currentNodeId = 0;

        public MockApiService() {
            _todos.Add(new Todo { Id = currentTodoId++, Title = "First todo", Description = "This is the first todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = true });
            _todos.Add(new Todo { Id = currentTodoId++, Title = "Second todo", Description = "This is the second todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });
            _todos.Add(new Todo { Id = currentTodoId++, Title = "Third todo", Description = "This is the third todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });

            _nodes.Add(new Node { Id = currentNodeId++, Content = "This is the first node", Title = "First node", CreatedAt = DateTime.Now, IsFavorite = true });
            _nodes.Add(new Node { Id = currentNodeId++, Content = "This is the second node", Title = "Second node", CreatedAt = DateTime.Now, IsFavorite = false });
            _nodes.Add(new Node { Id = currentNodeId++, Content = "This is the third node", Title = "Third node", CreatedAt = DateTime.Now, IsFavorite = false });
        }

        // -------------------- Todo API --------------------

        public List<TodoListItemReadDto> GetTodos() {
        // create niew istance of todosList so that the frontend update the ui
        // and simulate the api call
            List<TodoListItemReadDto> todosList = new();
            foreach (var todo in _todos) {
                todosList.Add(new TodoListItemReadDto {
                    Id = todo.Id,
                    Title = todo.Title,
                    Detline = todo.Detline,
                    HasDetline = todo.HasDetline,
                    IsCompleted = todo.IsCompleted
                });
            }
            return todosList;
        }

        public List<TodoListItemReadDto> GetNotCompletedTodos() {
            List<TodoListItemReadDto> todosList = new();
            _todos.Where(t => t.IsCompleted == false).ToList().ForEach(todo =>{
                todosList.Add(new TodoListItemReadDto
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Detline = todo.Detline,
                    HasDetline = todo.HasDetline,
                    IsCompleted = todo.IsCompleted
                });
            });
            return todosList;
        }

        public TodoReadDto GetTodoById(int id) {
            Todo? todo = _todos.Where(t => t.Id == id).First();
            if (todo == null)
            {
                // TODO add error
            }
            // create niew istance of todosList so that the frontend update the ui
            // and simulate the api call
            return new TodoReadDto {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Detline = todo.Detline,
                HasDetline = todo.HasDetline,
                IsCompleted = todo.IsCompleted
            };
        }

        public TodoReadDto UpdateTodo(TodoWriteDto todo) {
            if (todo == null) {
                return null; // TODO add error
            }

            Todo? existingTodo = _todos.Find(t => t.Id == todo.Id);
            if (existingTodo == null) {
                return null; // TODO add error
            }

            existingTodo.Title = todo.Title;
            existingTodo.Description = todo.Description;
            existingTodo.Detline = todo.Detline;
            existingTodo.HasDetline = todo.HasDetline;
            existingTodo.IsCompleted = todo.IsCompleted;

            // create niew istance of todosList so that the frontend update the ui
            // and simulate the api call
            return new TodoReadDto
            {
                Id = existingTodo.Id,
                Title = existingTodo.Title,
                Description = existingTodo.Description,
                Detline = existingTodo.Detline,
                HasDetline = existingTodo.HasDetline,
                IsCompleted = existingTodo.IsCompleted
            };
        }

        public void UpdateTodoState(int id, bool isCompleted) {
            Todo? todo = _todos.Find(t => t.Id == id);
            if (todo == null)
            {
                return; // TODo add error
            }
            todo.IsCompleted = isCompleted;
        }

        public void DeleteTodo(int id) {
            Todo? todo = _todos.Find(t => t.Id == id);
            if (todo == null)
            {
                return;
            }
            _todos.Remove(todo);
        }

        public TodoListItemReadDto AddTodo(TodoWriteDto todo) {
            Todo todonew = new Todo {
                Id = currentTodoId++,
                Title = todo.Title,
                Description = todo.Description,
                Detline = todo.Detline,
                HasDetline = todo.HasDetline,
                IsCompleted = todo.IsCompleted
            };
            _todos.Add(todonew);
            // create niew istance of todosList so that the frontend update the ui
            // and simulate the api call
            return new TodoListItemReadDto {
                Id = todonew.Id,
                Title = todonew.Title,
                Detline = todonew.Detline,
                HasDetline = todonew.HasDetline,
                IsCompleted = todonew.IsCompleted
            };
        }

        // -------------------- Node API --------------------
        public List<NodeListItemReadDto> GetNodes() {
            List<NodeListItemReadDto> nodesList = new();
            foreach (var node in _nodes) {
                nodesList.Add(new NodeListItemReadDto {
                    Id = node.Id,
                    Title = node.Title,
                    CreatedAt = node.CreatedAt,
                    IsFavorite = node.IsFavorite
                });
            }
            return nodesList;
        }

        public List<NodeListItemReadDto> GetFavoriteNodes() {
            List<NodeListItemReadDto> nodesList = new();
            _nodes.Where(n => n.IsFavorite == true).ToList().ForEach(node => {
                nodesList.Add(new NodeListItemReadDto {
                    Id = node.Id,
                    Title = node.Title,
                    CreatedAt = node.CreatedAt,
                    IsFavorite = node.IsFavorite
                });
            });
            return nodesList;
        }

        public NodeReadDto GetNodeById(int id) {
            Node? node = _nodes.Where(n => n.Id == id).First();
            if (node == null)
            {
                return null; // TODO add error
            }
            return new NodeReadDto
            {
                Id = node.Id,
                Title = node.Title,
                Content = node.Content,
                CreatedAt = node.CreatedAt
            };
        }

        public NodeReadDto UpdateNode(NodeWriteDto node) {
            if (node == null)
            {
                return null; // TODO add error
            }

            Node? existingNode = _nodes.Find(n => n.Id == node.Id);
            if (existingNode == null)
            {
                return null; // TODO add error
            }

            existingNode.Title = node.Title;
            existingNode.Content = node.Content;

            return new NodeReadDto
            {
                Id = existingNode.Id,
                Title = existingNode.Title,
                Content = existingNode.Content,
                CreatedAt = existingNode.CreatedAt
            };
        }

        public void DeleteNode(int id) {
            Node? node = _nodes.Find(n => n.Id == id);
            if (node == null) {
                return; // TODO add error
            }
            _nodes.Remove(node);
        }
        public NodeReadDto AddNode(NodeWriteDto node) {
            Node nodenew = new Node
            {
                Id = currentNodeId++,
                Title = node.Title,
                Content = node.Content,
                CreatedAt = DateTime.Now
            };
            _nodes.Add(nodenew);
            return new NodeReadDto
            {
                Id = nodenew.Id,
                Title = nodenew.Title,
                Content = nodenew.Content,
                CreatedAt = nodenew.CreatedAt
            };
        }

        public void ChageNodeFavorite(int id, bool isFavorite) {
            Node? node = _nodes.Find(n => n.Id == id);
            if (node == null) {
                return; // TODO add error
            }
            node.IsFavorite = isFavorite;
        }
    }
}
