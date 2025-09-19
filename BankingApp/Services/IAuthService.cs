using BankingApp.DTOs;

namespace BankingApp.Services
{
    public interface IAuthService
    {
        AuthResponseDto Login(LoginDto dto);
        AuthResponseDto Register(RegisterDto dto);
    }
}
