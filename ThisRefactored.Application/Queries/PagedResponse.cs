namespace ThisRefactored.Application.Queries;

public record PagedResult<T>(int TotalCount, IEnumerable<T> Items);