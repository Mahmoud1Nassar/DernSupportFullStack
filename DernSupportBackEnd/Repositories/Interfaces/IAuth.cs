using DernSupportBackEnd.Models.DTO;
using DernSupportBackEnd.Models;

namespace DernSupportBackEnd.Services
{
    public interface IAuth
    {
        Task<string> Register(RegisterModelDTO model);
        Task<string> Login(LoginModelDTO model);
        Task<string> GenerateJwtToken(ApplicationUser user);
        void Logout();
    }
}
