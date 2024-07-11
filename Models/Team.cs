using System.ComponentModel.DataAnnotations;

namespace MiniSimulator.Models
{
    public class Team
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(1,100)]
        public int Strength { get; set; }
        public string Image { get; set; }
    }
}
