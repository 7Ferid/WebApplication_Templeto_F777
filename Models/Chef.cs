using System.Runtime.CompilerServices;

namespace WebApplication_Templeto_F777.Models
{
    public class Chef
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }

    }
}
