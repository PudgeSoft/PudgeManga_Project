﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
using PudgeManga_Project.ViewModels;

namespace PudgeManga_Project.Controllers
{
    public class MangaController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMangaRepository<Manga, int> _mangaRepository;
        public MangaController(IMangaRepository<Manga, int> mangaRepository)
        {
            _mangaRepository = mangaRepository;
        }

        // GET: Mangas
        public async Task<IActionResult> Index()
        {
            var model = await _mangaRepository.GetAll();
            return View(model);
        }

        public async Task<IActionResult> MangaDetails(int mangaId)
        {
            var manga = await _mangaRepository.GetById(mangaId);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }
        public async Task<IActionResult> Reading(int mangaId)
        {
            var manga = await _mangaRepository.GetByIdReading(mangaId);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }
        public async Task<IActionResult> Chapters(int mangaId)
        {
            var manga = await _mangaRepository.GetByIdChapters(mangaId);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }
        public async Task<IActionResult> Comments(int mangaId)
        {
            var manga = await _mangaRepository.GetByIdComments(mangaId);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }
        public async Task<IActionResult> Description(int mangaId)
        {
            var manga = await _mangaRepository.GetById(mangaId);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }
    }
}
