
namespace SkillAssessment.Models
{
    public class Result
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int result_id { get; set; }
        public int TotalQuestions { get; set; }
        public int AnsweredQuestions { get; set; }
        public int UnansweredQuestions { get; set; }
        public int WrongAnsweredQuestions { get; set; }

        public string TimeLeft { get; set; }

        public int points { get; set; }

        public string passorfail { get; set; }

        public string date { get; set; }

        public string starttime { get; set; }

        public string endtime { get; set; }
        public Assessment? assessment { get; set; }
        public User? users { get; set; }
        public Topics? Topics { get; set; }
    }

}
