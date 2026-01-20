using System.ComponentModel.DataAnnotations;

namespace WebApplication_Templeto_F777.ViewModels.ChefViewModels
{
    public class ChefUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(256), MinLength(3)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(256), MinLength(3)]
        public string Description { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }

        public int ProfessionId { get; set; }
    }
}
