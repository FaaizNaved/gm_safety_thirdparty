namespace GM_Safety_Notification.Models;

public class Notification
{
    public int Id { get; set; }
    public string? Recipient { get; set; }
    public string? Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Sent { get; set; }
}
