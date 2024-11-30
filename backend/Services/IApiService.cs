using Shared.dto;
using Shared.Models;

namespace Backend.Services
{
    public interface IApiService {

        // Todo api methods
        public List<TodoListItemReadDto> GetTodos();
        public TodoReadDto GetTodoById(int id);
        public TodoReadDto UpdateTodo(TodoWriteDto todo);
        public void UpdateTodoState(int id, bool isCompleted);
        public void DeleteTodo(int id);
        public TodoListItemReadDto AddTodo(TodoWriteDto todo);

        // Node api methods
        public List<NodeListItemReadDto> GetNodes();
        public NodeReadDto GetNodeById(int id);
        public NodeReadDto UpdateNode(NodeWriteDto node);
        public void DeleteNode(int id);
        public NodeReadDto AddNode(NodeWriteDto node);
        public void ChageNodeFavorite(int id, bool isFavorite);
    }
}
