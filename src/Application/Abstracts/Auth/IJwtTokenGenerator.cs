using Domain.Entities;

namespace Application.Abstracts.Auth;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user, IEnumerable<string> roles);
}
