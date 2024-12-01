using Microsoft.AspNetCore.Mvc;
using Server.Repositories;
using Shared.dto;
using Shared.Models;

namespace Server.Controllers {
    [ApiController]
    [Route("api/node")]
    public class NoteController : Controller {
        INoTeRepo _nodeRepo;
        public NoteController(INoTeRepo nodeRepo) {
            _nodeRepo = nodeRepo;
        }

        [HttpGet]
        public ActionResult GetAllNodes() {
            IEnumerable<Note> nodes = _nodeRepo.GetAllNodes();
            // map to dto
            List<NoteListItemReadDto> readNodes = new();
            foreach (var node in nodes) {
                readNodes.Add(new NoteListItemReadDto {
                    Id = node.Id,
                    Title = node.Title,
                    CreatedAt = node.CreatedAt,
                    IsFavorite = node.IsFavorite
                });
            }
            return Ok(readNodes);
        }

        [HttpGet("favorite")]
        public ActionResult GetFavoriteNodes() {
            IEnumerable<Note> nodes = _nodeRepo.GetFavoriteNodes();
            // map to dto
            List<NoteListItemReadDto> readNodes = new();
            foreach (var node in nodes) {
                readNodes.Add(new NoteListItemReadDto {
                    Id = node.Id,
                    Title = node.Title,
                    CreatedAt = node.CreatedAt,
                    IsFavorite = node.IsFavorite
                });
            }
            return Ok(readNodes);
        }

        [HttpGet("{id}")]
        public ActionResult GetNodeById(int id) {
            try {
                Note node = _nodeRepo.GetNodeById(id);
                // map to dto
                return Ok(new NoteReadDto {
                    Id = node.Id,
                    Title = node.Title,
                    Content = node.Content,
                    CreatedAt = node.CreatedAt,
                });
            } catch (KeyNotFoundException e) {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateNode(NoteWriteDto newNode) {
            Note node = _nodeRepo.CreateNode(newNode.Title, newNode.Content);
            return Ok(new NoteReadDto {
                Id = node.Id,
                Title = node.Title,
                Content = node.Content,
                CreatedAt = node.CreatedAt
            });
        }

        [HttpPut("{id}")]
        public ActionResult UpdateNodeById(int id, NoteWriteDto wNode) {
            try {
                Note uNode = new Note {
                    Id = id,
                    Title = wNode.Title,
                    Content = wNode.Content
                };

                _nodeRepo.UpdateNodeById(uNode);
                return Ok("Update node");
            } catch (KeyNotFoundException e) {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}/favorite")]
        public ActionResult ChangeNodeFavorite(int id, bool isFavorite) {
            try {
                _nodeRepo.ChangeNodeFavorite(id, isFavorite);
                return Ok("Change node favorite");
            } catch (KeyNotFoundException e) {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteNodeById(int id) {
            try {
                _nodeRepo.DeleteNodeById(id);
                return Ok("Delete node");
            } catch (KeyNotFoundException e) {
                return NotFound(e.Message);
            }
        }

    }
}
