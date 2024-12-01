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

        public List<NoteListItemReadDto> GetNodes() {
            var response = _httpClient.GetAsync("http://localhost:5110/api/node").Result;
            response.EnsureSuccessStatusCode();
            // Todo error handling
            var nodes = response.Content.ReadFromJsonAsync<List<NoteListItemReadDto>>().Result;
            return nodes;
        }
        public List<NoteListItemReadDto> GetFavoriteNodes() {
            var response = _httpClient.GetAsync("http://localhost:5110/api/node/favorite").Result;
            response.EnsureSuccessStatusCode();
            // Todo error handling
            var nodes = response.Content.ReadFromJsonAsync<List<NoteListItemReadDto>>().Result;
            return nodes;
        }
        public NoteReadDto GetNodeById(int id) {
            var response = _httpClient.GetAsync($"http://localhost:5110/api/node/{id}").Result;
            response.EnsureSuccessStatusCode();
            // Todo error handling
            var node = response.Content.ReadFromJsonAsync<NoteReadDto>().Result;
            return node;
        }
        public NoteReadDto CreateNode(NoteWriteDto node) {
            var response = _httpClient.PostAsJsonAsync("http://localhost:5110/api/node", node).Result;
            response.EnsureSuccessStatusCode();
            // Todo error handling
            var createdNode = response.Content.ReadFromJsonAsync<NoteReadDto>().Result;
            return createdNode;
        }
        public void UpdateNode(NoteWriteDto node, int id) {
            var response = _httpClient.PutAsJsonAsync($"http://localhost:5110/api/node/{id}", node).Result;
            response.EnsureSuccessStatusCode();
            // Todo error handling
        }
        public void DeleteNode(int id) {
            var response = _httpClient.DeleteAsync($"http://localhost:5110/api/node/{id}").Result;
            response.EnsureSuccessStatusCode();
            // Todo error handling
        }
        public void ChageNodeFavorite(int id, bool isFavorite) {
            var content = JsonContent.Create(new { });
            var response = _httpClient.PutAsync($"http://localhost:5110/api/node/{id}/favorite?isFavorite={isFavorite}", content).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
