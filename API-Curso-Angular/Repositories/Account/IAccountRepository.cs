using API_Curso_Angular.DTOs;
using API_Curso_Angular.DTOs.Request;
using API_Curso_Angular.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace API_Curso_Angular.Repositories.Account {
    public interface IAccountRepository {

        Task<ResultDTO<string>> RegisterCustomer(CreateAccountRequestDTO model);
        Task<ResultDTO<string>> RegisterAdmin(CreateAccountRequestDTO model);
        Task<ResultDTO<string>> ForgotPassword(ForgotPasswordRequestDTO model);
        Task<ResultDTO<string>> ResetPassword(ResetPasswordRequestDTO model);
        Task<ResultDTO<string>> ConfirmEmail(long userId, string code);
        Task<ResultDTO<LoginResponseDTO>> Login(LoginRequestDTO model);
    }
}
