using Shared.dto;
using System.Net.Http.Json;

namespace Backend.Services {
    public class ApiTodoService : IApiTodoService {
        private readonly HttpClient _httpClient;

        public string BaseAddress { get; set; } = "http://localhost:5110";

        public ApiTodoService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<TodoListItemReadDto>> GetTodosAsync() {
            try {
                var response = await _httpClient.GetAsync($"{BaseAddress}/api/todo");
                var todos = await response.Content.ReadFromJsonAsync<List<TodoListItemReadDto>>();
                return todos;
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }

        public async Task<List<TodoListItemReadDto>> GetNotCompletedTodosAsync() {
            try {
                var response = await _httpClient.GetAsync($"{BaseAddress}/api/todo/not-completed");
                var todos = await response.Content.ReadFromJsonAsync<List<TodoListItemReadDto>>();
                return todos;
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }

        public async Task<TodoReadDto> GetTodoByIdAsync(int id) {
            try {
                var response = await _httpClient.GetAsync($"{BaseAddress}/api/todo/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new NotFoundException(id);
                }
                var todo = await response.Content.ReadFromJsonAsync<TodoReadDto>();
                return todo;
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }

        public TodoReadDto GetTodoById(int id) {
            try {
                var response = _httpClient.GetAsync($"{BaseAddress}/api/todo/{id}").Result;
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new NotFoundException(id);
                }
                var todo = response.Content.ReadFromJsonAsync<TodoReadDto>().Result;
                return todo;
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }


        public async Task<TodoListItemReadDto> CreateTodoAsync(TodoWriteDto todo) {
            try {
                var response = await _httpClient.PostAsJsonAsync($"{BaseAddress}/api/api/todo", todo);
                var createdTodo = await response.Content.ReadFromJsonAsync<TodoListItemReadDto>();
                return createdTodo;
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }

        public async Task<TodoReadDto> UpdateTodoAsync(TodoWriteDto todo, int id) {
            try {
                var response = await _httpClient.PutAsJsonAsync($"{BaseAddress}/api/todo/{id}", todo);
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new NotFoundException(id);
                }
                var updatedTodo = await response.Content.ReadFromJsonAsync<TodoReadDto>();
                return updatedTodo;
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }

        public async Task UpdateTodoStateAsync(int id, bool isCompleted) {
            try {
                var content = JsonContent.Create(new {});
                var response = await _httpClient.PutAsync($"http://localhost:5110/api/todo/{id}/state?isCompleted={isCompleted}", content);
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new NotFoundException(id);
                }
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }

        public async Task DeleteTodoAsync(int id) {
            try {
                var response = await _httpClient.DeleteAsync($"http://localhost:5110/api/todo/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new NotFoundException(id);
                }
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }
    }
}
