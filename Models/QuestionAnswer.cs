
namespace SkillAssessment.Models
{
    public class QuestionAnswer
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public virtual Questions Questions { get; set; }

        public int SelectedOption { get; set; }

        public int topic_id { get; set; }
    }
}
