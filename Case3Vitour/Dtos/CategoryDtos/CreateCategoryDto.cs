using System.ComponentModel.DataAnnotations;

namespace Case3Vitour.Dtos.CategoryDtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Kategori adı boş geçilemez.")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir.")]
        public string CategoryName { get; set; } = null!;

        public bool CategoryStatus { get; set; } = true;
    }
}