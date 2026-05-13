using API_Curso_Angular.Data;
using API_Curso_Angular.Models;
using API_Curso_Angular.Models.Auth;
using Microsoft.AspNetCore.Identity;


namespace API_Curso_Angular.Repositories.Account {

    public class AccountRepository : IAccountRepository {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AppDataContext _context;

        public AccountRepository(UserManager<User> userManager, RoleManager<Role> roleManager, AppDataContext context) {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IdentityResult> AddRoles(User user, string role) {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IList<string>> GetRoles(User user) {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> CreateUser(User user, string password) {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<string> GeneratePasswordResetToken(User user) {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<User?> GetUserByEmail(string email) {
            return await _userManager.FindByEmailAsync(email);

        }

        public async Task<User?> GetUserById(long id) {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<IdentityResult> ResetPassword(User user, string token, string newPassword) {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<bool> CheckPassword(User user, string novaSenha) {
            return await _userManager.CheckPasswordAsync(user, novaSenha);
        }

        public async Task<IdentityResult> ConfirmEmail(User user, string token) {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task CriarCliente(Cliente cliente) {
            await _context.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<IdentityResult> AddToRole(User user, string role) {
            if (! await _roleManager.RoleExistsAsync(role)) {
                await _roleManager.CreateAsync(new Role { Name = role });
            }
            return await _userManager.AddToRoleAsync(user, role);
        }
    }
}
