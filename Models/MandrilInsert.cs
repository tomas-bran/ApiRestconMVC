using System.ComponentModel.DataAnnotations;

namespace MandrilAPI.Models
{
    public class MandrilInsert
    {
        [Required]
        [MaxLength(30)]
        public string? Nombre {  get; set; }

        [Required]
        [MaxLength(30)]
        public string? Apellido { get; set; }
    }
}
