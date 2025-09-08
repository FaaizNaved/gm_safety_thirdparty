namespace GM_Safety_Functional.Models;

public class FunctionalTask
{
    public int Id { get; set; }
    public string? Module { get; set; }
    public string? TaskName { get; set; }
    public DateTime? Deadline { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
