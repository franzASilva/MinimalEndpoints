namespace MinimalEndpoints.Domain.Model;

public sealed record HealthCheckReportModel(string Check, string Status, string? ErrorMessage, int Duration_ms, string? Description);
