namespace WebApplication_Templeto_F777.Models
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Chef> Chefs { get; set; } = [];

    }
}
