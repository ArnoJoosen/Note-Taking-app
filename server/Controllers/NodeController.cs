using Microsoft.AspNetCore.Mvc;
using Server.Repositories;
using Shared.dto;
using Shared.Models;

namespace Server.Controllers {
    [ApiController]
    [Route("api/node")]
    public class NodeController : Controller {
        INodeRepo _nodeRepo;
        public NodeController(INodeRepo nodeRepo) {
            _nodeRepo = nodeRepo;
        }

        [HttpGet]
        public ActionResult GetAllNodes() {
            IEnumerable<Node> nodes = _nodeRepo.GetAllNodes();
            // map to dto
            List<NodeListItemReadDto> readNodes = new();
            foreach (var node in nodes) {
                readNodes.Add(new NodeListItemReadDto {
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
            IEnumerable<Node> nodes = _nodeRepo.GetFavoriteNodes();
            // map to dto
            List<NodeListItemReadDto> readNodes = new();
            foreach (var node in nodes) {
                readNodes.Add(new NodeListItemReadDto {
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
                Node node = _nodeRepo.GetNodeById(id);
                // map to dto
                return Ok(new NodeReadDto {
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
        public ActionResult CreateNode(NodeWriteDto newNode) {
            Node node = _nodeRepo.CreateNode(newNode.Title, newNode.Content);
            return Ok(new NodeReadDto {
                Id = node.Id,
                Title = node.Title,
                Content = node.Content,
                CreatedAt = node.CreatedAt
            });
        }

        [HttpPut("{id}")]
        public ActionResult UpdateNodeById(int id, NodeWriteDto wNode) {
            try {
                Node uNode = new Node {
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
