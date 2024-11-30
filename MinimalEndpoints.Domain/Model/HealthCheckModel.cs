namespace MinimalEndpoints.Domain.Model;

public sealed record HealthCheckModel(string StatusApplication, IEnumerable<HealthCheckReportModel> HealthChecks);
