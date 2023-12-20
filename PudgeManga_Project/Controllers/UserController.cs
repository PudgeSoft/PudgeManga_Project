﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Interfaces;
using PudgeManga_Project.Models;
using PudgeManga_Project.Models.Repositories;
using PudgeManga_Project.ViewModels;
using RunGroopWebApp.Repository;

namespace PudgeManga_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IGoogleDriveAPIRepository<IFormFile> _googleDriveAPIRepository;


        public UserController(IUserRepository userRepository,
            UserManager<User> userManager,
            IGoogleDriveAPIRepository<IFormFile> googleDriveAPIRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _googleDriveAPIRepository = googleDriveAPIRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    ProfileImageUrl = user.Image,
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }

            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                State = user.State,
                UserName = user.UserName,
                Age = user.Age,
                Aboutme = user.Aboutme,
                ProfileImageUrl = user.Image,
            };
            return View(userDetailViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            var editMV = new EditProfileViewModel()
            {
                //City = User.City,
                //    ProfileImageUrl = user.ProfileImageUrl,
                Age = user.Age,
                Aboutme = user.Aboutme
            };

            return View(editMV);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditProfile", editVM);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            //if (editVM.Image != null) // only update profile image
            //{
            //    var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

            //    if (photoResult.Error != null)
            //    {
            //        ModelState.AddModelError("Image", "Failed to upload image");
            //        return View("EditProfile", editVM);
            //    }

            //    if (!string.IsNullOrEmpty(user.ProfileImageUrl))
            //    {
            //        _ = _photoService.DeletePhotoAsync(user.ProfileImageUrl);
            //    }

            // user.ProfileImageUrl = photoResult.Url.ToString();
            //editVM.ProfileImageUrl = user.ProfileImageUrl;

            //    await _userManager.UpdateAsync(user);

            //    return View(editVM);
            //}

            //user.City = editVM.City;
            user.State = editVM.State;
            user.Age = editVM.Age;
            user.Aboutme = editVM.Aboutme;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Detail", "User", new { user.Id });
        }

        [HttpPost, ActionName("UploadProfilePicture")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file, string userId)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);
                if (user == null)
                {
                    return NotFound();
                }

                string folderName = $"User_{userId}_ProfilePictures";
                string folderId = _googleDriveAPIRepository.GetOrCreateFolder(folderName);
                _googleDriveAPIRepository.UploadFileToGoogleDrive(file, folderId);

                var fileLink = _googleDriveAPIRepository.GetModifiedFileLinks(folderId);
                await _userRepository.UpdateProfilePictureLink(fileLink[0], userId);

                return RedirectToAction("Profile", new { id = userId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при завантаженні фотографії профілю на Google Drive: {ex.Message}");
                return RedirectToAction("Index");
            }
        }



    }
}