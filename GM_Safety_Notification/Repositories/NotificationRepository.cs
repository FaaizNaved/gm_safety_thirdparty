using GM_Safety_Notification.Data;
using GM_Safety_Notification.Models;
using Microsoft.EntityFrameworkCore;

namespace GM_Safety_Notification.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly NotificationDbContext _db;
    public NotificationRepository(NotificationDbContext db) => _db = db;

    public Task<List<Notification>> GetAllAsync(CancellationToken ct = default)
        => _db.Notifications.AsNoTracking().OrderByDescending(x => x.CreatedAt).ToListAsync(ct);

    public Task<Notification?> GetAsync(int id, CancellationToken ct = default)
        => _db.Notifications.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<Notification> AddAsync(Notification entity, CancellationToken ct = default)
    {
        _db.Notifications.Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<Notification?> UpdateAsync(int id, Notification entity, CancellationToken ct = default)
    {
        var existing = await _db.Notifications.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (existing is null) return null;

        existing.Recipient = entity.Recipient;
        existing.Message = entity.Message;
        existing.Sent = entity.Sent;

        await _db.SaveChangesAsync(ct);
        return existing;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var existing = await _db.Notifications.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (existing is null) return false;
        _db.Notifications.Remove(existing);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
