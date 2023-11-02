using SkillAssessment.Models;


public class User
{
    [Key]
    public int User_ID { get; set; }

    [StringLength(50)]
    public string? User_FirstName { get; set; }

    [StringLength(50)]
    public string? User_LastName { get; set; }

    [StringLength(100)]
    public string? User_Departmenr { get; set; }

    [StringLength(100)]
    public string? User_Designation { get; set; }

    [DataType(DataType.Date)]
    public string? User_DOB { get; set; }

    public string? User_Location { get; set; }

    [StringLength(200)]
    public string? User_Address { get; set; }

    [StringLength(10)]
    public string? User_Gender { get; set; }

    [StringLength(100)]
    public string? User_EduLevel { get; set; }

    public long? User_PhoneNo { get; set; }

    [StringLength(100)]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? User_Email { get; set; }

    public string? User_Password { get; set; }
    public string? User_Image { get; set; }
    public ICollection<Assessment>? assessments { get; set; }
    public ICollection<Result>? results { get; set; }
}
