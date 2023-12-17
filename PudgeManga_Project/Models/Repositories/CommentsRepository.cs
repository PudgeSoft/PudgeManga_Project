using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.ViewModels;

namespace PudgeManga_Project.Models.Repositories
{
    public class CommentRepository :ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public IQueryable<Comment> GetAll()
        {
            return _context.Comments.OrderBy(x => x.CommentDate);
        }

        public CommentViewModel AddComment(CommentViewModel comment)
        {
            var newComment = new Comment
            {
                ParentId = comment.ParentId,
                CommentText = comment.CommentText,
                CommentDate = DateTime.Now
                // Додайте інші необхідні властивості
            };

            _context.Comments.Add(newComment);
            _context.SaveChanges();

            // Повертаємо CommentViewModel на основі збереженого коментаря
            var addedComment = _context.Comments
                .Where(c => c.CommentId == newComment.CommentId)
                .Select(c => new CommentViewModel
                {
                    CommentId = c.CommentId,
                    ParentId = c.ParentId,
                    CommentText = c.CommentText,
                    CommentDate = c.CommentDate
                    // Додайте інші необхідні властивості
                })
                .FirstOrDefault();

            return addedComment;
        }
        public async Task<IEnumerable<Comment>> GetCommentsByMangaId(int mangaId)
        {
            try
            {
                var comments = await _context.CommentsForManga
                    .Where(mc => mc.MangaId == mangaId)
                    .Select(mc => mc.Comment)
                    .ToListAsync();

                return comments;
            }
            catch (Exception ex)
            {
                // Обробити помилку
                Console.WriteLine($"Помилка отримання коментарів для манги {mangaId}: {ex.Message}");
                return null; // або повернути пустий список або власну логіку обробки помилок
            }
        }



    }
}
