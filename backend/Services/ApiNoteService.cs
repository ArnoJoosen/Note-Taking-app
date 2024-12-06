using Backend.Services;
using Shared.dto;
using System.Net.Http.Json;

namespace backend.Services {
    public class ApiNoteService : IApiNoteService {
        private readonly HttpClient _httpClient;

        public ApiNoteService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<NoteListItemReadDto>> GetNodesAsync() {
            var response = await _httpClient.GetAsync("http://localhost:5110/api/node");
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            }
            var nodes = await response.Content.ReadFromJsonAsync<List<NoteListItemReadDto>>();
            return nodes;
        }
        public async Task<List<NoteListItemReadDto>> GetFavoriteNodesAsync() {
            var response = await _httpClient.GetAsync("http://localhost:5110/api/node/favorite");
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            }
            var nodes = await response.Content.ReadFromJsonAsync<List<NoteListItemReadDto>>();
            return nodes;
        }
        public async Task<NoteReadDto> GetNodeByIdAsync(int id) {
            var response = await _httpClient.GetAsync($"http://localhost:5110/api/node/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new Exception("Note not found");
            }
            var node = await response.Content.ReadFromJsonAsync<NoteReadDto>();
            return node;
        }

        public NoteReadDto GetNodeById(int id) {
            var response = _httpClient.GetAsync($"http://localhost:5110/api/node/{id}").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new Exception("Note not found");
            }
            var node = response.Content.ReadFromJsonAsync<NoteReadDto>().Result;
            return node;
        }

        public async Task<NoteReadDto> CreateNodeAsync(NoteWriteDto node) {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5110/api/node", node);
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            }
            var createdNode = await response.Content.ReadFromJsonAsync<NoteReadDto>();
            return createdNode;
        }
        public async Task UpdateNodeAsync(NoteWriteDto node, int id) {
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5110/api/node/{id}", node);
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new Exception("Note not found");
            }
        }
        public async Task DeleteNodeAsync(int id) {
            var response = await _httpClient.DeleteAsync($"http://localhost:5110/api/node/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new Exception("Note not found");
            }
        }
        public async Task ChageNodeFavoriteAsync(int id, bool isFavorite) {
            var content = JsonContent.Create(new { });
            var response = _httpClient.PutAsync($"http://localhost:5110/api/node/{id}/favorite?isFavorite={isFavorite}", content).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) {
                throw new Exception("Connection error - service unavailable");
            } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new Exception("Note not found");
            }
        }
    }
}
