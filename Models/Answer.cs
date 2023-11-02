namespace SkillAssessment.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
