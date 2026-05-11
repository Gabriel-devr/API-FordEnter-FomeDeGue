using API_Curso_Angular.Data;
using API_Curso_Angular.DTOs;
using API_Curso_Angular.DTOs.Request.Account;
using API_Curso_Angular.DTOs.Response;
using API_Curso_Angular.Models;
using API_Curso_Angular.Models.Auth;
using API_Curso_Angular.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace API_Curso_Angular.Repositories.Account {

    public class AccountRepository : IAccountRepository {

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        public AccountRepository(AppDataContext context, UserManager<User> userManager, IConfiguration configuration, TokenService tokenService) {
            _userManager = userManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<ResultDTO<string>> ForgotPassword(ForgotPasswordRequestDTO model) {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.IsEmailConfirmedAsync(user)) {
                return new ResultDTO<string>("Erro");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var frontendUrl = _configuration["Frontend:ResetPasswordUrl"] ?? "http://localhost:4200/reset-password";
            var callbackUrl = $"{frontendUrl}?token={code}&email={Uri.EscapeDataString(user.Email)}";

            var html = $"<p>Redefina sua senha clicando <a href=\"{callbackUrl}\">aqui</a>.</p>";

            return new ResultDTO<string>("Se o e-mail estiver cadastrado e confirmado, um link de recuperação foi enviado.");
        }

        public async Task<ResultDTO<LoginResponseDTO>> Login(LoginRequestDTO model) {
            
            var user = await _userManager.FindByEmailAsync(model.Email);
            //Verificamos se o email está no nosso banco de dados

            if(user == null) {
                return new ResultDTO<LoginResponseDTO>("Usuário ou senha inválidos");
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            //Verifica se o email checado corresponde a sua senha

            if (!checkPassword) {
                return new ResultDTO<LoginResponseDTO>("Usuário ou senha inválidos");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles);

            var loginResponse = new LoginResponseDTO() {
                Token = token,
                User = new UserResponseDTO() {
                    Email = user.Email!,
                    FullName = user.NomeCompleto
                }
            };
            return new ResultDTO<LoginResponseDTO>(loginResponse);
        }

        public async Task<ResultDTO<string>> RegisterCustomer(CreateAccountRequestDTO model) {

            var user = new User() {
                Email = model.Email,
                NomeCompleto = model.Nome,
                UserName = model.Nome
            };

            var createUser = await _userManager.CreateAsync(user, model.Password);

            if(!createUser.Succeeded) {
                return new ResultDTO<string>(createUser.Errors.Select(x => x.Description).ToList());
            }

            return new ResultDTO<string>("Conta criada com sucesso", new List<string>());

        }

        public async Task<ResultDTO<string>> RegisterAdmin (CreateAccountRequestDTO model) {

            var user = new User() {
                Email = model.Email,
                NomeCompleto = model.Nome,
                UserName = model.Nome
            };

            var createUser = await _userManager.CreateAsync(user, model.Password);

            if (!createUser.Succeeded) {
                return new ResultDTO<string>(createUser.Errors.Select(x => x.Description).ToList());
            }

            return new ResultDTO<string>("Conta criada com sucesso", new List<string>());
        }

        public async Task<ResultDTO<string>> ResetPassword(ResetPasswordRequestDTO model) {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null) {
                return new ResultDTO<string>("Usuário ou senha inválidos.");
            }

            var resetPassword = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (!resetPassword.Succeeded) {
                return new ResultDTO<string>(resetPassword.Errors.Select(x=>x.Description).ToList());
            }

            return new ResultDTO<string>("Senha redefinida com sucesso", new List<string>());
        }

        public async Task<ResultDTO<string>> ConfirmEmail(long userId, string code) {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) {
                return new ResultDTO<string>("Usuário não encontrado");
            }

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded) {
                return new ResultDTO<string>(result.Errors.Select(x => x.Description).ToList());
            }

            return new ResultDTO<string>("E-mail confirmado com sucesso");
        }
    }
}
