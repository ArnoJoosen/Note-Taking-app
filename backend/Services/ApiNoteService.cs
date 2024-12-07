using Backend.Services;
using Shared.dto;
using System.Net.Http.Json;

namespace backend.Services {
    public class ApiNoteService : IApiNoteService {
        private readonly HttpClient _httpClient;
        public string BaseAddress { get; set; } = "http://localhost:5110";

        public ApiNoteService(HttpClient httpClient, string baseUrl = "http://localhost:5110") {
            _httpClient = httpClient;
            BaseAddress = baseUrl;
        }

        public async Task<List<NoteListItemReadDto>> GetNodesAsync() {
            try {
                var response = await _httpClient.GetAsync($"{BaseAddress}/api/node/");
                var nodes = await response.Content.ReadFromJsonAsync<List<NoteListItemReadDto>>();
                return nodes;
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }
        public async Task<List<NoteListItemReadDto>> GetFavoriteNodesAsync() {
            try {
                var response = await _httpClient.GetAsync($"{BaseAddress}/api/node/favorite");
                var nodes = await response.Content.ReadFromJsonAsync<List<NoteListItemReadDto>>();
                return nodes ?? new List<NoteListItemReadDto>(); // if null return empty list
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }
        public async Task<NoteReadDto> GetNodeByIdAsync(int id) {
            try {
                var response = await _httpClient.GetAsync($"{BaseAddress}/api/node/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new NotFoundException(id);
                }
                var node = await response.Content.ReadFromJsonAsync<NoteReadDto>();
                return node;
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }

        public NoteReadDto GetNodeById(int id) {
            try {
                var response = _httpClient.GetAsync($"{BaseAddress}/api/node/{id}").Result;
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new NotFoundException(id);
                }
                var node = response.Content.ReadFromJsonAsync<NoteReadDto>().Result;
                return node;
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }

        public async Task<NoteReadDto> CreateNodeAsync(NoteWriteDto node) {
            try {
                var response = await _httpClient.PostAsJsonAsync($"{BaseAddress}/api/node", node);
                var createdNode = await response.Content.ReadFromJsonAsync<NoteReadDto>();
                return createdNode;
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }
        public async Task UpdateNodeAsync(NoteWriteDto node, int id) {
            try {
                var response = await _httpClient.PutAsJsonAsync($"{BaseAddress}/api/node/{id}", node);
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new NotFoundException(id);
                }
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }
        public async Task DeleteNodeAsync(int id) {
            try {
                var response = await _httpClient.DeleteAsync($"{BaseAddress}/api/node/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new NotFoundException(id);
                }
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }
        public async Task ChageNodeFavoriteAsync(int id, bool isFavorite) {
            try {
                var content = JsonContent.Create(new { }); // Todo remove this
                var response = _httpClient.PutAsync($"{BaseAddress}/api/node/{id}/favorite?isFavorite={isFavorite}", content).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new NotFoundException(id);
                }
            } catch (HttpRequestException) {
                throw new ConnectionErrorException();
            }
        }
    }
}
