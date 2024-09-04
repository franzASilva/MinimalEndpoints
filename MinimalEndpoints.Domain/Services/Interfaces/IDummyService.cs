using MinimalEndpoints.Domain.Model;

namespace MinimalEndpoints.Domain.Services.Interfaces;

public interface IDummyService
{
    Task<DummyModel[]> GetAllAsync(CancellationToken ct);
    Task<List<DummyModel>> GetCompleteAsync(CancellationToken ct);
    Task<DummyModel?> GetAsync(long id, CancellationToken ct);
    Task<DummyModel?> CreateAsync(DummyModel dummyModel, CancellationToken ct);
    Task<DummyModel?> UpdateAsync(DummyModel dummyModel, CancellationToken ct);
    Task<int?> DeleteAsync(long id, CancellationToken ct);
}
