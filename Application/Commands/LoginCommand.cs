using Application.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;
public record LoginCommand(string Username, string PlainPassword) : IRequest<Guid>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Guid>
{
    private readonly BimboContext _context;

    public LoginCommandHandler(BimboContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(x => x.Username.Equals(request.Username) && x.PlainPassword.Equals(request.PlainPassword))
            .SingleOrDefaultAsync();

        if (user != null)
        {
            return user.Id;
        }

        var newUser = new User(request.Username, request.PlainPassword);
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser.Id;
    }
}
