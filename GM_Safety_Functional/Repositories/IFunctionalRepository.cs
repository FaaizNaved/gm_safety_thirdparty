using GM_Safety_Functional.Models;

namespace GM_Safety_Functional.Repositories;

public interface IFunctionalRepository
{
    Task<List<FunctionalTask>> GetAllAsync(CancellationToken ct = default);
    Task<FunctionalTask?> GetAsync(int id, CancellationToken ct = default);
    Task<FunctionalTask> AddAsync(FunctionalTask task, CancellationToken ct = default);
    Task<FunctionalTask?> UpdateAsync(int id, FunctionalTask task, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
