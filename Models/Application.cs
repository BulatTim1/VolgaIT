using System.ComponentModel.DataAnnotations;

namespace VolgaIT.Models
{
    public class Application
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "ID приложения")]
        public string? AppId { get; set; }

        [Required]
        [Display(Name = "Название приложения")]
        public string? Name { get; set; }

        [Display(Name = "Дата создания")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public string? UserId { get; set; }

        public List<AppEvent>? AppEvents { get; set; }
    }
}
