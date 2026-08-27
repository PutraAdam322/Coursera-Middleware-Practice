using System.ComponentModel.DataAnnotations;
public class UserDTO
 {
    [Required]
     public string Username { get; set; } = String.Empty;
     [Required]
     [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
     public string Password { get; set; } = String.Empty;
 }