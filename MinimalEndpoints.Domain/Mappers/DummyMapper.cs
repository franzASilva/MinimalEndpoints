using MinimalEndpoints.Domain.Entities;
using MinimalEndpoints.Domain.Model;

namespace MinimalEndpoints.Domain.Mappers;

public static class DummyMapper
{
    public static Dummy ToEntity(DummyModel dummyModel)
    {
        return new Dummy
        {
            Name = dummyModel.Name,
            IsComplete = dummyModel.IsComplete
        };
    }

    public static Dummy ToEntity(DummyModel dummyModel, Dummy dummy)
    {
        dummy.Name = dummyModel.Name;
        dummy.IsComplete = dummyModel.IsComplete;
        return dummy;
    }

    public static DummyModel ToModel(Dummy dummy)
    {
        return new DummyModel
        (
            dummy.Id,
            dummy.Name,
            dummy.IsComplete
        );
    }
}
