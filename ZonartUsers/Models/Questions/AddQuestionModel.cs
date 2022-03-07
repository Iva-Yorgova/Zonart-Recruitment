using System.ComponentModel.DataAnnotations;

namespace ZonartUsers.Models.Questions
{
    public class AddQuestionModel
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}
