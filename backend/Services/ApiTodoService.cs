using Shared.dto;
using System.Net.Http.Json;

namespace Backend.Services {
    public class ApiTodoService : IApiTodoService {
        private readonly HttpClient _httpClient;

        public ApiTodoService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<TodoListItemReadDto>> GetTodosAsync() {
            var response = await _httpClient.GetAsync("http://localhost:5110/api/todo");
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            }
            var todos = await response.Content.ReadFromJsonAsync<List<TodoListItemReadDto>>();
            return todos;
        }

        public async Task<List<TodoListItemReadDto>> GetNotCompletedTodosAsync() {
            var response = await _httpClient.GetAsync("http://localhost:5110/api/todo/not-completed");
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            }
            var todos = await response.Content.ReadFromJsonAsync<List<TodoListItemReadDto>>();
            return todos;
        }

        public async Task<TodoReadDto> GetTodoByIdAsync(int id) {
            var response = await _httpClient.GetAsync($"http://localhost:5110/api/todo/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new Exception("Note not found");
            }
            var todo = await response.Content.ReadFromJsonAsync<TodoReadDto>();
            return todo;
        }

        public TodoReadDto GetTodoById(int id) {
            var response = _httpClient.GetAsync($"http://localhost:5110/api/todo/{id}").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new Exception("Note not found");
            }
            var todo = response.Content.ReadFromJsonAsync<TodoReadDto>().Result;
            return todo;
        }


        public async Task<TodoListItemReadDto> CreateTodoAsync(TodoWriteDto todo) {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5110/api/todo", todo);
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            }
            var createdTodo = await response.Content.ReadFromJsonAsync<TodoListItemReadDto>();
            return createdTodo;
        }

        public async Task<TodoReadDto> UpdateTodoAsync(TodoWriteDto todo, int id) {
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5110/api/todo/{id}", todo);
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new Exception("Note not found");
            }
            var updatedTodo = await response.Content.ReadFromJsonAsync<TodoReadDto>();
            return updatedTodo;
        }

        public async Task UpdateTodoStateAsync(int id, bool isCompleted) {
            var content = JsonContent.Create(new {});
            var response = await _httpClient.PutAsync($"http://localhost:5110/api/todo/{id}/state?isCompleted={isCompleted}", content);
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new Exception("Note not found");
            }
        }

        public async Task DeleteTodoAsync(int id) {
            var response = await _httpClient.DeleteAsync($"http://localhost:5110/api/todo/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new Exception("Note not found");
            }
        }
    }
}
