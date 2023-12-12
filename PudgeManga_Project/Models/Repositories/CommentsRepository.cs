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
            var _comment = new Comment()
            {
                ParentId = comment.ParentId,
                CommentText = comment.CommentText,
                CommentDate = DateTime.Now
                
            };

            _context.Comments.Add(_comment);
            _context.SaveChanges();

            return _context.Comments.Where(x => x.CommentId == _comment.CommentId)
                    .Select(x => new CommentViewModel
                    {
                        CommentId = x.CommentId,
                        CommentText = x.CommentText,
                        ParentId = x.ParentId,
                        CommentDate = x.CommentDate

                    }).FirstOrDefault();
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
