using backend.Dtos.Comment;
using backend.Models;

namespace backend.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                Id = comment.Id,
                StockId = comment.StockId,
                Title = comment.Title,
            };
        }

        public static Comment ToCommentFromCreateDto(this CreateCommentDto createCommentDto, int stockId)
        {
            return new Comment
            {
                Content = createCommentDto.Content,
                Title = createCommentDto.Title,
                StockId = stockId,
            };
        }
    }
}