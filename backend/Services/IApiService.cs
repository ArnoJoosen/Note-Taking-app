using Shared.dto;

namespace Backend.Services
{
    public interface IApiService {

        // Todo api methods
        public List<TodoListItemReadDto> GetTodos();
        public List<TodoListItemReadDto> GetNotCompletedTodos();
        public TodoReadDto GetTodoById(int id);
        public TodoListItemReadDto CreateTodo(TodoWriteDto todo);
        public TodoReadDto UpdateTodo(TodoWriteDto todo, int id);
        public void UpdateTodoState(int id, bool isCompleted);
        public void DeleteTodo(int id);

        // Node api methods
        public List<NodeListItemReadDto> GetNodes();
        public List<NodeListItemReadDto> GetFavoriteNodes();
        public NodeReadDto GetNodeById(int id);
        public NodeReadDto CreateNode(NodeWriteDto node);
        public void UpdateNode(NodeWriteDto node, int id);
        public void DeleteNode(int id);
        public void ChageNodeFavorite(int id, bool isFavorite);
    }
}
