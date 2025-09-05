using GM_Safety_Notification.Models;

namespace GM_Safety_Notification.Repositories;

public interface INotificationRepository
{
    Task<List<Notification>> GetAllAsync(CancellationToken ct = default);
    Task<Notification?> GetAsync(int id, CancellationToken ct = default);
    Task<Notification> AddAsync(Notification entity, CancellationToken ct = default);
    Task<Notification?> UpdateAsync(int id, Notification entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
