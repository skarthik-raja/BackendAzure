
namespace SkillAssessment.Models
{
    public class Topics
    {
        [Key] 
        public int Topic_Id { get; set; }
        public string Topic_Name { get; set; }
        public ICollection<Questions>? Questions { get; set; }
        public ICollection<Assessment>? Assessment { get; set; } 
        public ICollection<Result>? Results { get; set; }
        public ICollection<Level>? Levels { get; set; }
    }
}
