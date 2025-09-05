using GM_Safety_Functional.Data;
using GM_Safety_Functional.Models;
using Microsoft.EntityFrameworkCore;

namespace GM_Safety_Functional.Repositories;

public class FunctionalRepository : IFunctionalRepository
{
    private readonly FunctionalDbContext _db;

    public FunctionalRepository(FunctionalDbContext db) => _db = db;

    public Task<List<FunctionalTask>> GetAllAsync(CancellationToken ct = default)
        => _db.FunctionalTasks.AsNoTracking().OrderByDescending(x => x.CreatedAt).ToListAsync(ct);

    public Task<FunctionalTask?> GetAsync(int id, CancellationToken ct = default)
        => _db.FunctionalTasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<FunctionalTask> AddAsync(FunctionalTask task, CancellationToken ct = default)
    {
        _db.FunctionalTasks.Add(task);
        await _db.SaveChangesAsync(ct);
        return task;
    }

    public async Task<FunctionalTask?> UpdateAsync(int id, FunctionalTask task, CancellationToken ct = default)
    {
        var existing = await _db.FunctionalTasks.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (existing is null) return null;

        existing.Module = task.Module;
        existing.TaskName = task.TaskName;
        existing.Deadline = task.Deadline;
        existing.IsCompleted = task.IsCompleted;

        await _db.SaveChangesAsync(ct);
        return existing;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var existing = await _db.FunctionalTasks.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (existing is null) return false;
        _db.FunctionalTasks.Remove(existing);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
