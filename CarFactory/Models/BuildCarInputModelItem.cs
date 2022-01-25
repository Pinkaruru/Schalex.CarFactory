using System.ComponentModel.DataAnnotations;
using static CarFactory.Controllers.CarController;

namespace CarFactory.Models
{
    public class BuildCarInputModelItem
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public CarSpecificationInputModel Specification { get; set; }
    }
}
