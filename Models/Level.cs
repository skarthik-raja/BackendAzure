namespace SkillAssessment.Models
{
    public class Level
    {
        [Key]
        public int LevelId { get; set; }

        public string LevelName { get; set; }

        public ICollection<Questions>? Questions { get; set; }
        public ICollection<Assessment>? Assessment { get; set; }

       
    }
}
