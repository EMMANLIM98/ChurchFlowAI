namespace ChurchFlow.Application.Members.GetMembers;

public class GetMembersQuery
{
    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 20;

    public string SortBy { get; init; } = "LastName";

    public string SortDirection { get; init; } = "asc";
}