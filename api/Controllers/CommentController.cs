using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Interfaces;
using api.Mappers;

using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();

            var commentDtos = comments.Select(comment => comment.ToCommentDto());

            return Ok(commentDtos);
        }
        
    }
}