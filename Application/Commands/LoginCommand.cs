using Application.Persistence;
using Domain.Entities;
using Infrastructure.OneOf.Types;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Application.Commands;
public record LoginCommand(string Username, string PlainPassword) : IRequest<OneOf<Success<Guid>, BadRequest>>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, OneOf<Success<Guid>, BadRequest>>
{
    private readonly BimboContext _context;

    public LoginCommandHandler(BimboContext context)
    {
        _context = context;
    }

    public async Task<OneOf<Success<Guid>, BadRequest>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(x => x.Username.Equals(request.Username))
            .SingleOrDefaultAsync();

        if (user != null)
        {
            if (user.PlainPassword == request.PlainPassword)
            {
                return new Success<Guid>(user.Id);
            }
            else
            {
                return new BadRequest("Username already exists");
            }
        }


        var newUser = new User(request.Username, request.PlainPassword);
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return new Success<Guid>(newUser.Id);
    }
}
