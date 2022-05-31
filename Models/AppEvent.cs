using System.ComponentModel.DataAnnotations;

namespace VolgaIT.Models
{
    public class AppEvent
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "ID приложения")]
        public string? AppId { get; set; }

        [Required]
        [Display(Name = "Название события")]
        public string? Event { get; set; }

        [Display(Name = "Дополнительная информация")]
        public string? Info { get; set; }
        [Display(Name = "Дата создания")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
    }
}
