using API_Curso_Angular.DTOs;
using API_Curso_Angular.DTOs.Request.Account;
using API_Curso_Angular.DTOs.Response;
using API_Curso_Angular.Models;
using API_Curso_Angular.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_Curso_Angular.Repositories.Account {
    public interface IAccountRepository {

        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(long id);

        Task<IdentityResult> AddRoles(User user, string role);
        Task<IList<string>> GetRoles(User user);

        Task<IdentityResult> CreateUser(User user, string password);

        Task<IdentityResult> ResetPassword(User user, string token, string newPassword );

        Task<string> GeneratePasswordResetToken(User user);

        Task<bool> CheckPassword(User user, string novaSenha);
        Task<IdentityResult> ConfirmEmail(User user, string token);
        Task CriarCliente(Cliente cliente);
        Task<IdentityResult> AddToRole(User user, string role);
    }
}
