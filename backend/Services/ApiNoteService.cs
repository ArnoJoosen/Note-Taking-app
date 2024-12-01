using Backend.Services;
using Shared.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services {
    public class ApiNoteService : IApiNoteService {
        private readonly HttpClient _httpClient;

        public ApiNoteService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<NoteListItemReadDto>> GetNodesAsync() {
            var response = await _httpClient.GetAsync("http://localhost:5110/api/node");
            response.EnsureSuccessStatusCode();
            // Todo error handling
            var nodes = await response.Content.ReadFromJsonAsync<List<NoteListItemReadDto>>();
            return nodes;
        }
        public async Task<List<NoteListItemReadDto>> GetFavoriteNodesAsync() {
            var response = await _httpClient.GetAsync("http://localhost:5110/api/node/favorite");
            response.EnsureSuccessStatusCode();
            // Todo error handling
            var nodes = await response.Content.ReadFromJsonAsync<List<NoteListItemReadDto>>();
            return nodes;
        }
        public async Task<NoteReadDto> GetNodeByIdAsync(int id) {
            var response = await _httpClient.GetAsync($"http://localhost:5110/api/node/{id}");
            response.EnsureSuccessStatusCode();
            // Todo error handling
            var node = await response.Content.ReadFromJsonAsync<NoteReadDto>();
            return node;
        }

        public NoteReadDto GetNodeById(int id) {
            var response = _httpClient.GetAsync($"http://localhost:5110/api/node/{id}").Result;
            response.EnsureSuccessStatusCode();
            // Todo error handling
            var node = response.Content.ReadFromJsonAsync<NoteReadDto>().Result;
            return node;
        }

        public async Task<NoteReadDto> CreateNodeAsync(NoteWriteDto node) {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5110/api/node", node);
            response.EnsureSuccessStatusCode();
            // Todo error handling
            var createdNode = await response.Content.ReadFromJsonAsync<NoteReadDto>();
            return createdNode;
        }
        public async Task UpdateNodeAsync(NoteWriteDto node, int id) {
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5110/api/node/{id}", node);
            response.EnsureSuccessStatusCode();
            // Todo error handling
        }
        public async Task DeleteNodeAsync(int id) {
            var response = await _httpClient.DeleteAsync($"http://localhost:5110/api/node/{id}");
            response.EnsureSuccessStatusCode();
            // Todo error handling
        }
        public async Task ChageNodeFavoriteAsync(int id, bool isFavorite) {
            var content = JsonContent.Create(new { });
            var response = _httpClient.PutAsync($"http://localhost:5110/api/node/{id}/favorite?isFavorite={isFavorite}", content).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
