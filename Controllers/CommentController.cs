using backend.Dtos.Comment;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentDtoList = comments.Select((comment) => comment.ToCommentDto());

            return Ok(commentDtoList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        [Route("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
            if (!await _stockRepository.StockExistsAsync(stockId))
            {
                return BadRequest("Stock does not exist");
            }

            var comment = commentDto.ToCommentFromCreateDto(stockId);
            await _commentRepository.CreateAsync(comment);

            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto commentDto)
        {
            var comment = await _commentRepository.UpdateAsync(id, commentDto);

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteAsync(id);

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return NoContent();
        }
    }
}