using GM_Safety_Notification.Models;

namespace GM_Safety_Notification.Services;

public interface INotificationService
{
    Task<IEnumerable<Notification>> GetAllAsync(CancellationToken ct = default);
    Task<Notification?> GetAsync(int id, CancellationToken ct = default);
    Task<Notification> CreateAsync(Notification entity, CancellationToken ct = default);
    Task<Notification?> UpdateAsync(int id, Notification entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
