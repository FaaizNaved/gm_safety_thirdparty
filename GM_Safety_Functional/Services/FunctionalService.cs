using GM_Safety_Functional.Models;
using GM_Safety_Functional.Repositories;

namespace GM_Safety_Functional.Services;

public class FunctionalService : IFunctionalService
{
    private readonly IFunctionalRepository _repo;
    public FunctionalService(IFunctionalRepository repo) => _repo = repo;

    public Task<IEnumerable<FunctionalTask>> GetAllAsync(CancellationToken ct = default) => _repo.GetAllAsync(ct).ContinueWith(t => (IEnumerable<FunctionalTask>)t.Result, ct);
    public Task<FunctionalTask?> GetAsync(int id, CancellationToken ct = default) => _repo.GetAsync(id, ct);
    public Task<FunctionalTask> CreateAsync(FunctionalTask task, CancellationToken ct = default) => _repo.AddAsync(task, ct);
    public Task<FunctionalTask?> UpdateAsync(int id, FunctionalTask task, CancellationToken ct = default) => _repo.UpdateAsync(id, task, ct);
    public Task<bool> DeleteAsync(int id, CancellationToken ct = default) => _repo.DeleteAsync(id, ct);
}
