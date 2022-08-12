using Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public record AttemptQueryResponse(string Username, long Seconds, string Comment, DateTimeOffset DateOfExecution, long? Improvement);
public record AttemptQuery(Guid? UserId) : IRequest<List<AttemptQueryResponse>>;

public class AttemptQueryHandler : IRequestHandler<AttemptQuery, List<AttemptQueryResponse>>
{
    private readonly BimboContext _context;

    public AttemptQueryHandler(BimboContext context)
    {
        _context = context;
    }

    public async Task<List<AttemptQueryResponse>> Handle(AttemptQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Challenges.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(x => x.UserId == request.UserId.Value);
        }

        query = query
            .OrderByDescending(x => x.DateOfExecution)
            .Take(100);

        return await query
            .Select(x => new AttemptQueryResponse(x.User!.Username, x.Seconds, x.Comment, x.DateOfExecution, x.Improvement))
            .ToListAsync();
    }
}
