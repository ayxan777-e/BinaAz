using Application.DTOs.Auth;

namespace Application.Abstracts.Auth;

public interface IAuthService
{
    Task<TokenResponse> LoginAsync(LoginRequest request);
}
