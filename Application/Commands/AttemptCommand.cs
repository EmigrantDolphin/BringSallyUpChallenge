using Application.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;
public record AttemptCommand(long Seconds, string Comment, Guid UserId) : IRequest<Unit>;

public class AttemptCommandHandler : IRequestHandler<AttemptCommand, Unit>
{
    private readonly BimboContext _context;

    public AttemptCommandHandler(BimboContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AttemptCommand request, CancellationToken cancellationToken)
    {
        var doesTodaysEntryExist = await _context.Challenges
            .Where(x => x.UserId == request.UserId)
            .Where(x => x.DateOfExecution > DateTimeOffset.UtcNow.Date)
            .AnyAsync();

        if (doesTodaysEntryExist)
        {
            await UpdateTodaysEntry(request);
        }
        else
        {
            await CreateTodaysEntry(request);
        }

        return Unit.Value;
    }
    private async Task CreateTodaysEntry(AttemptCommand request)
    {
        var lastAttempt = await _context.Challenges
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.DateOfExecution)
            .FirstOrDefaultAsync();

        Challenge newAttempt;

        if (lastAttempt != null)
        {
            var improvement = request.Seconds - lastAttempt.Seconds;
            newAttempt = new Challenge(request.UserId, request.Seconds, request.Comment, improvement);
        }
        else
        {
            newAttempt = new Challenge(request.UserId, request.Seconds, request.Comment);
        }


        _context.Challenges.Add(newAttempt);
        await _context.SaveChangesAsync();
    }

    private async Task UpdateTodaysEntry(AttemptCommand request)
    {
        var lastTwoEntries = await _context.Challenges
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.DateOfExecution)
            .Take(2)
            .ToListAsync();

        var todaysEntry = lastTwoEntries.FirstOrDefault();
        var beforeTodayEntry = lastTwoEntries.Count == 2 ? lastTwoEntries.Last() : null;

        if (todaysEntry != null)
        {
            if (beforeTodayEntry != null)
            {
                var improvement = todaysEntry.Seconds - beforeTodayEntry.Seconds;
                todaysEntry.UpdateSeconds(request.Seconds, request.Comment, improvement);
            }
            else
            {
                todaysEntry.UpdateSeconds(request.Seconds, request.Comment);
            }
        }

        await _context.SaveChangesAsync();
    }
}
