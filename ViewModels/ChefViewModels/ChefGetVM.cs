using System.ComponentModel.DataAnnotations;
using WebApplication_Templeto_F777.Models;

namespace WebApplication_Templeto_F777.ViewModels.ChefViewModels
{
    public class ChefGetVM
    {
        public int Id { get; set; }
      
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

        public string ProfessionName { get; set; } = string.Empty;
  
    }
}
