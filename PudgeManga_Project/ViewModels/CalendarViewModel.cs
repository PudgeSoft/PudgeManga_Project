using PudgeManga_Project.Models;

namespace PudgeManga_Project.ViewModels
{
    public class CalendarViewModel
    {
        public DateTime PublicationDate { get; set; }
        public List<Chapter> Chapters { get; set; }
    }
}
