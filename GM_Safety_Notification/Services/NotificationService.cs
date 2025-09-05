using GM_Safety_Notification.Models;
using GM_Safety_Notification.Repositories;

namespace GM_Safety_Notification.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repo;
    public NotificationService(INotificationRepository repo) => _repo = repo;

    public Task<IEnumerable<Notification>> GetAllAsync(CancellationToken ct = default) => _repo.GetAllAsync(ct).ContinueWith(t => (IEnumerable<Notification>)t.Result, ct);
    public Task<Notification?> GetAsync(int id, CancellationToken ct = default) => _repo.GetAsync(id, ct);
    public Task<Notification> CreateAsync(Notification entity, CancellationToken ct = default) => _repo.AddAsync(entity, ct);
    public Task<Notification?> UpdateAsync(int id, Notification entity, CancellationToken ct = default) => _repo.UpdateAsync(id, entity, ct);
    public Task<bool> DeleteAsync(int id, CancellationToken ct = default) => _repo.DeleteAsync(id, ct);
}
