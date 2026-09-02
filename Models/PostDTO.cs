using System.ComponentModel.DataAnnotations;
public class PostDTO{
    [Required]
    public string Title { get; set; } = String.Empty;
    [Required]
    public string Content { get; set; } = String.Empty;
    public int UserId { get; set; }
}