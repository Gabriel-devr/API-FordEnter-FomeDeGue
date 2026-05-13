using API_Curso_Angular.DTOs;
using API_Curso_Angular.DTOs.Request.Account;
using API_Curso_Angular.DTOs.Response;

namespace API_Curso_Angular.Services.Accounts {
    public interface IAccountService {
        Task<ResultDTO<UserResponseDTO>> RegisterCustomer(CreateAccountRequestDTO model);
        Task<ResultDTO<UserResponseDTO>> RegisterAdmin(CreateAccountRequestDTO model);
        Task<ResultDTO<string>> ForgotPassword(ForgotPasswordRequestDTO model);
        Task<ResultDTO<string>> ResetPassword(ResetPasswordRequestDTO model);
        Task<ResultDTO<string>> ConfirmEmail(long userId, string code);
        Task<ResultDTO<LoginResponseDTO>> Login(LoginRequestDTO model);
    }
}
