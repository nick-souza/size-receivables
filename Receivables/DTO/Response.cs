namespace Receivables.DTO.Outgoing;

public record Response(bool success, string? error = null);
public record Response<T>(bool success, T data);