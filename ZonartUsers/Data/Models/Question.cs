using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Data.Models
{
    public class Question
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Answer { get; set; }

    }
}
