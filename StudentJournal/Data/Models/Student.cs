using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentJournal.Data.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Institute { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Group { get; set; } = string.Empty;

        [Required]
        [Range(1, 5)]
        public int Course { get; set; } = 1; // Значение по умолчанию - 1 курс

        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}