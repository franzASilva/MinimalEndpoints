using MinimalEndpoints.Domain.Entities;
using MinimalEndpoints.Domain.Mappers;
using MinimalEndpoints.Domain.Model;
using MinimalEndpoints.Domain.Repositories.Interfaces;
using MinimalEndpoints.Domain.Services.Interfaces;

namespace MinimalEndpoints.Domain.Services;

public sealed class DummyService(IDummyRepository dummyRepository) : IDummyService
{
    public async Task<DummyModel[]> GetAllAsync(CancellationToken ct)
    {
        var dummies = await dummyRepository.GetAllAsync(ct);    
        
        if (dummies.Length != 0)
        {
            return dummies.Select(d => DummyMapper.ToModel(d)).ToArray();
        }

        return [];
    }

    public async Task<List<DummyModel>> GetCompleteAsync(CancellationToken ct)
    {
        var dummies = await dummyRepository.GetCompleteAsync(ct);

        if (dummies.Count != 0)
        {
            return dummies.Select(d => DummyMapper.ToModel(d)).ToList();
        }

        return [];
    }

    public async Task<DummyModel?> GetAsync(long id, CancellationToken ct)
    {
        if (await dummyRepository.GetAsync(id, ct) is Dummy dummy)
        {
            return DummyMapper.ToModel(dummy);
        }

        return null;
    }

    public async Task<DummyModel?> CreateAsync(DummyModel dummyModel, CancellationToken ct)
    {
        var dummy = DummyMapper.ToEntity(dummyModel);
        await dummyRepository.CreateAsync(dummy, ct);
        return DummyMapper.ToModel(dummy);
    }

    public async Task<DummyModel?> UpdateAsync(DummyModel dummyModel, CancellationToken ct)
    {
        if (await dummyRepository.GetAsync(dummyModel.Id, ct) is Dummy dummy)
        {
            dummy = DummyMapper.ToEntity(dummyModel, dummy);
            await dummyRepository.UpdateAsync(dummy, ct);
            return DummyMapper.ToModel(dummy);
        }

        return null;
    }

    public async Task<int?> DeleteAsync(long id, CancellationToken ct) => await dummyRepository.DeleteAsync(id, ct);
}
