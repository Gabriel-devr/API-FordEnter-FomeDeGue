using API_Curso_Angular.DTOs;
using API_Curso_Angular.DTOs.Request;
using API_Curso_Angular.Extensions;
using API_Curso_Angular.Repositories.Account;
using Microsoft.AspNetCore.Mvc;

namespace API_Curso_Angular.Controllers {


    [ApiController]
    [Route("v1/[controller]")]
    public class AccountController : ControllerBase {

        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository) {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("registrar-cliente")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CreateAccountRequestDTO model) {
            if (!ModelState.IsValid) {
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));
            }

            var createAccount = await _accountRepository.RegisterCustomer(model);

            if (createAccount.Errors.Any()) {
                return BadRequest(createAccount); //Erro detectado no repo
            }

            return Ok(createAccount);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model) {
            if (!ModelState.IsValid) {
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));
            }

            var login = await _accountRepository.Login(model);

            if (login.Errors.Any()) {
                return BadRequest(login);
            }

            return Ok(login);
        }

        [HttpPost]
        [Route("recuperar-senha")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDTO model) {
            if (!ModelState.IsValid) {
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));
            }

            var forgotPassword = await _accountRepository.ForgotPassword(model);

            return Ok(forgotPassword);

        }

        [HttpPost]
        [Route("redefinir-senha")]
        public async Task<IActionResult> Login([FromBody] ResetPasswordRequestDTO model) {
            if (!ModelState.IsValid) {
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));
            }

            var changePassword = await _accountRepository.ResetPassword(model);

            if (changePassword.Errors.Any()) {
                return BadRequest(changePassword);
            }

            return Ok(changePassword);

        }

        [HttpGet]
        [Route("confirmar-email")]
        public async Task<IActionResult> ConfirmEmail(long userId, string code) {
            if (!ModelState.IsValid) {
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));
            }

            var confirmEmail = await _accountRepository.ConfirmEmail(userId, code);

            if (confirmEmail.Errors.Any()) {
                return BadRequest(confirmEmail);
            }

            return Ok(confirmEmail);
        }
    }
}