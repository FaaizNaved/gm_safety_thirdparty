using GM_Safety_Functional.Models;

namespace GM_Safety_Functional.Services;

public interface IFunctionalService
{
    Task<IEnumerable<FunctionalTask>> GetAllAsync(CancellationToken ct = default);
    Task<FunctionalTask?> GetAsync(int id, CancellationToken ct = default);
    Task<FunctionalTask> CreateAsync(FunctionalTask task, CancellationToken ct = default);
    Task<FunctionalTask?> UpdateAsync(int id, FunctionalTask task, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
