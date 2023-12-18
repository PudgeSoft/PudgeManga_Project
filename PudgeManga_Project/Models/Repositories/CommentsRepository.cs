﻿using Google.Apis.Drive.v3.Data;
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
        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        public async Task AddMangaCommentAsync(MangaComment mangaComment)
        {
            _context.CommentsForManga.Add(mangaComment);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .OrderBy(x => x.CommentDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Comment>> GetCommentsForMangaAsync(int mangaId)
        {
            return await _context.CommentsForManga
                .Where(mc => mc.MangaId == mangaId)
                .Select(mc => mc.Comment)
                .Include(c => c.ParentComment)
                .ToListAsync();
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
