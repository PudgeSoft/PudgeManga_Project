﻿using PudgeManga_Project.Models;
using PudgeManga_Project.ViewModels;
namespace PudgeManga_Project.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> AddCommentAsync(Comment comment);
        Task<List<Comment>> GetAllAsync();
        Task<IEnumerable<Comment>> GetCommentsByMangaId(int mangaId);
    }

}
