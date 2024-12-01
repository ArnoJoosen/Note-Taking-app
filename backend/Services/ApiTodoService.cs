using Shared.dto;
using System.Net.Http.Json;

namespace Backend.Services {
    public class ApiTodoService : IApiTodoService {
        private readonly HttpClient _httpClient;

        public ApiTodoService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public List<TodoListItemReadDto> GetTodos() {
            var response = _httpClient.GetAsync("http://localhost:5110/api/todo").Result;
            response.EnsureSuccessStatusCode();
            var todos = response.Content.ReadFromJsonAsync<List<TodoListItemReadDto>>().Result;
            return todos;
        }

        public List<TodoListItemReadDto> GetNotCompletedTodos() {
            var response = _httpClient.GetAsync("http://localhost:5110/api/todo/not-completed").Result;
            response.EnsureSuccessStatusCode();
            var todos = response.Content.ReadFromJsonAsync<List<TodoListItemReadDto>>().Result;
            return todos;
        }

        public TodoReadDto GetTodoById(int id) {
            var response = _httpClient.GetAsync($"http://localhost:5110/api/todo/{id}").Result;
            response.EnsureSuccessStatusCode();
            var todo = response.Content.ReadFromJsonAsync<TodoReadDto>().Result;
            return todo;
        }

        public TodoListItemReadDto CreateTodo(TodoWriteDto todo) {
            var response = _httpClient.PostAsJsonAsync("http://localhost:5110/api/todo", todo).Result;
            response.EnsureSuccessStatusCode();
            var createdTodo = response.Content.ReadFromJsonAsync<TodoListItemReadDto>().Result;
            return createdTodo;
        }

        public TodoReadDto UpdateTodo(TodoWriteDto todo, int id) {
            var response = _httpClient.PutAsJsonAsync($"http://localhost:5110/api/todo/{id}", todo).Result;
            response.EnsureSuccessStatusCode();
            var updatedTodo = response.Content.ReadFromJsonAsync<TodoReadDto>().Result;
            return updatedTodo;
        }

        public void UpdateTodoState(int id, bool isCompleted) {
            var content = JsonContent.Create(new {});
            var response = _httpClient.PutAsync($"http://localhost:5110/api/todo/{id}/state?isCompleted={isCompleted}", content).Result;
            response.EnsureSuccessStatusCode();
        }

        public void DeleteTodo(int id) {
            var response = _httpClient.DeleteAsync($"http://localhost:5110/api/todo/{id}").Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
