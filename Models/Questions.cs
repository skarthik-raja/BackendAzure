
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace SkillAssessment.Models
{
    public class Questions
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int QnId { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string QnInWords { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Option1 { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Option2 { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Option3 { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Option4 { get; set; }
        public string Explanation { get; set; }
        public int Answer { get; set; }
        public Topics? topics { get; set; }
        public Level? Levels { get; set; }
    }
}
