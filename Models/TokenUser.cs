
public class TokenUser
{
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string User_Email { get; set; }

  
    public string User_Password { get; set; }
}
