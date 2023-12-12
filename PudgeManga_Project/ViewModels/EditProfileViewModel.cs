namespace PudgeManga_Project.ViewModels
{
    public class EditProfileViewModel
    {
        public string? UserName { get; set; }
        public int? Mileage { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? State { get; set; }
        public int Age { get; set; }
        public string? Aboutme { get; set; }
        public IFormFile? Image { get; set; }
    }
}