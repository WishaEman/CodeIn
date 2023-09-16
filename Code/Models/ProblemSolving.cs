using System.ComponentModel.DataAnnotations;

namespace Code.Models
{
    public partial class ProblemSolving
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
    }
}
