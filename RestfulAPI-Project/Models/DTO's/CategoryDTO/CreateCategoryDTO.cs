using System.ComponentModel.DataAnnotations;

namespace RestfulAPI_Project.Models.DTO_s.CategoryDTO
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Kategori adı zorunludur!!")]
        [MinLength(3, ErrorMessage = "Minimum 3 karakter olmak zorunda!!")]
        [RegularExpression(@"^[a-zA-Z- ]+$", ErrorMessage = "Sadece harf girebilirsiniz!!!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Kategori açıklaması zorunludur!!")]
        [MinLength(3, ErrorMessage = "Minimum 3 karakter olmak zorunda!!")]
        public string Description { get; set; }
    }
}
