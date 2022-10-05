using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SciMaterials.Auth.Requests;

namespace SciMaterials.Auth.Controllers;

/// <summary>
/// Контроллер для регистрации и авторизации в системе
/// </summary>
[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _UserManager;
    private readonly SignInManager<IdentityUser> _SignInManager;
    private readonly RoleManager<IdentityRole> _RoleManager;
    private readonly ILogger<AccountController> _Logger;

    public AccountController(
        UserManager<IdentityUser> UserManager,
        SignInManager<IdentityUser> SignInManager,
        RoleManager<IdentityRole> RoleManager,
        ILogger<AccountController> logger)
    {
        _UserManager = UserManager;
        _SignInManager = SignInManager;
        _RoleManager = RoleManager;
        _Logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRequest RegisterUserRequest)
    {
        //Это временная проверка
        if (string.IsNullOrEmpty(RegisterUserRequest.Email)
            && string.IsNullOrEmpty(RegisterUserRequest.Password)
            && string.IsNullOrEmpty(RegisterUserRequest.PhoneNumber))
            return BadRequest("Некорректно введены данные пользователя");

        try
        {
            var user = new IdentityUser
            {
                Email = RegisterUserRequest.Email,
                UserName = RegisterUserRequest.Email,
                PhoneNumber = RegisterUserRequest.PhoneNumber,
            };

            var result = await _UserManager.CreateAsync(user, RegisterUserRequest.Password);
            await _UserManager.AddToRoleAsync(user, "user");
            await _SignInManager.SignInAsync(user, false);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _Logger.LogError(ex, "Ошибка - Пользователя не удалось зарегистрировать");
        }

        return BadRequest("Пользователя не удалось зарегистрировать");

    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(string email, string password)
    {
        //Это временная проверка
        if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password)) 
            return BadRequest("Некорректно введены данные пользователя");

        try
        {
            var identity_result = await _UserManager.FindByEmailAsync(email);
            if (identity_result is not null)
            {
                var sign_in_result = await _SignInManager.PasswordSignInAsync(
                    userName: email,
                    password: password,
                    isPersistent: true,
                    lockoutOnFailure: false);

                if (sign_in_result.Succeeded)
                {
                    return Ok(sign_in_result);
                }

                return BadRequest("Не удалось авторизовать пользователя");
            }
        }
        catch (Exception ex)
        {
            _Logger.LogError(ex, "Ошибка - Не удалось авторизовать пользователя");
        }

        return BadRequest("Не удалось авторизовать пользователя");

    }

    [AllowAnonymous]
    [HttpPost("Logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        try
        {
            await _SignInManager.SignOutAsync();
            return Ok("Пользователь вышел из системы");
        }
        catch (Exception ex)
        {
            _Logger.LogError(ex, "Ошибка - Не удалось выйти из системы");
            return BadRequest("Не удалось выйти из системы");
        }
    }

    [Authorize(Roles = "admin, user")]
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(string OldPassword, string NewPassword)
    {
        if (!string.IsNullOrEmpty(OldPassword) || !string.IsNullOrEmpty(NewPassword))
        {
            try
            {
                var current_user_name = User.Identity!.Name;
                var identity_user = await _UserManager.FindByNameAsync(current_user_name);
                if (!string.IsNullOrEmpty(current_user_name) || identity_user is not null)
                {
                    var identity_result = await _UserManager.ChangePasswordAsync(
                        identity_user,
                        OldPassword,
                        NewPassword);
                    if (identity_result.Succeeded)
                    {
                        return Ok(identity_result);
                    }

                    return BadRequest("Не удалось изменить пароль");
                }

                return BadRequest("Не удалось получить информацию о пользователе");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Ошибка");
                return BadRequest("Произошла ошибка при смене пароля");
            }
        }

        return BadRequest("Некорректно введены данные");
    }

    [Authorize(Roles = "admin")]
    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRoleAsync(string RoleName)
    {
        //Это временная проверка
        if (!string.IsNullOrEmpty(RoleName))
        {
            try
            {
                var result = await _RoleManager.CreateAsync(new IdentityRole(RoleName));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Ошибка");
                return BadRequest("Произошла ошибка при создании роли");
            }
        }

        return BadRequest("Некорректно введены данные");
    }

    [Authorize(Roles = "admin")]
    [HttpGet("GetAllRoles")]
    public async Task<IActionResult> GetAllRolesAsync()
    {
        try
        {
            var result = await _RoleManager.Roles.ToListAsync();
            if (result is not { Count: > 0 })
            {
                return Ok(result);
            }

            return BadRequest("Не удалось получить список ролей");
        }
        catch (Exception ex)
        {
            _Logger.LogError(ex, "Ошибка");
            return BadRequest("Произошла ошибка при запросе ролей");
        }
    }

    [Authorize(Roles = "admin")]
    [HttpGet("GetRoleById")]
    public async Task<IActionResult> GetRoleByIdAsync(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            try
            {
                var result = await _RoleManager.FindByIdAsync(id);
                if (result is not null)
                {
                    return Ok(result);
                }

                return BadRequest("Не удалось получить роль");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Ошибка");
                return BadRequest("Произошла ошибка при запросе роли");
            }
        }

        return BadRequest("Некорректно введены данные пользователя");
    }

    [Authorize(Roles = "admin")]
    [HttpPost("EditRoleById")]
    public async Task<IActionResult> EditRoleByIdAsync(string id, string RoleName)
    {
        //Это временная проверка
        if (!string.IsNullOrEmpty(id) || !string.IsNullOrEmpty(RoleName))
        {
            try
            {
                var found_role = await _RoleManager.FindByIdAsync(id);
                found_role.Name = RoleName;

                var result = await _RoleManager.UpdateAsync(found_role);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Ошибка");
                return BadRequest("Произошла ошибка при редактировании роли");
            }
        }

        return BadRequest("Некорректно введены данные пользователя");
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("DeleteRoleById")]
    public async Task<IActionResult> DeleteRoleByIdAsync(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            try
            {
                var result = await _RoleManager.FindByIdAsync(id);
                if (result is not null)
                {
                    var user_role = await _RoleManager.DeleteAsync(result);
                    return Ok(user_role);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Ошибка");
                return BadRequest("Произошла ошибка при удалении роли");
            }
        }

        return BadRequest("Некорректно введены данные пользователя");
    }

    [Authorize(Roles = "admin")]
    [HttpPost("AddUserRole")]
    public async Task<IActionResult> AddUserRoleAsync(string email, string RoleName)
    {
        //Это временная проверка
        if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(RoleName))
        {
            try
            {
                var found_user = await _UserManager.FindByEmailAsync(email);
                var user_roles = await _UserManager.GetRolesAsync(found_user);

                var roles = await _RoleManager.Roles.ToListAsync();
                if (!user_roles.Contains(RoleName))
                {
                    var is_role_contains_in_system = roles.Select(x => x.Name.Contains(RoleName.ToLower()));
                    foreach (var is_role in is_role_contains_in_system)
                    {
                        if (is_role)
                        {
                            var role_added_result = await _UserManager.AddToRoleAsync(found_user, RoleName.ToLower());
                            if (role_added_result.Succeeded)
                            {
                                return Ok(role_added_result);
                            }
                        }
                    }
                }

                return BadRequest("Некорректно введены данные");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Ошибка");
                return BadRequest("Произошла ошибка при добавлении роли пользователю");
            }
        }

        return BadRequest("Некорректно введены данные");
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("DeleteUserRole")]
    public async Task<IActionResult> DeleteUserRoleAsync(string email, string RoleName)
    {
        //Это временная проверка
        if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(RoleName))
        {
            try
            {
                var found_user = await _UserManager.FindByEmailAsync(email);
                var user_roles = await _UserManager.GetRolesAsync(found_user);

                var roles = await _RoleManager.Roles.ToListAsync();
                if (user_roles.Contains(RoleName))
                {
                    var is_role_contains_in_system = roles.Select(x => x.Name.Contains(RoleName));
                    foreach (var is_role in is_role_contains_in_system)
                    {
                        if (is_role)
                        {
                            var role_removed_result = await _UserManager.RemoveFromRoleAsync(found_user, RoleName);
                            if (role_removed_result.Succeeded)
                            {
                                return Ok(role_removed_result);
                            }
                        }
                    }
                }

                return BadRequest("Некорректно введены данные");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Ошибка");
                return BadRequest("Произошла ошибка при удалении роли пользователю");
            }
        }

        return BadRequest("Некорректно введены данные");
    }

    [Authorize(Roles = "admin")]
    [HttpGet("ListOfUserRoles")]
    public async Task<IActionResult> ListOfUserRolesAsync(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            try
            {
                var found_user = await _UserManager.FindByEmailAsync(email);
                if (found_user is not null)
                {
                    var user_roles = await _UserManager.GetRolesAsync(found_user);
                    if (user_roles is not null)
                    {
                        return Ok(user_roles.ToList());
                    }

                    return BadRequest("Не удалось получить список ролей");
                }

                return BadRequest("Данного пользователя нет в системе, либо некорректно введены данные пользователя");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Ошибка");
                return BadRequest("Произошла ошибка при получении списка ролей пользователей");
            }
        }

        return BadRequest("Некорректно введены данные");
    }

    [Authorize(Roles = "admin")]
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserRequest create)
    {
        //Это временная проверка
        if (!string.IsNullOrEmpty(create.Email) ||
            !string.IsNullOrEmpty(create.Password) ||
            !string.IsNullOrEmpty(create.PhoneNumber))
        {
            await RegisterAsync(create);
            return Ok("Пользователь успешно создан в системе");
        }

        return BadRequest("Некорректно введены данные пользователя");
    }

    [Authorize(Roles = "admin")]
    [HttpPost("GetUserByEmail")]
    public async Task<IActionResult> GetUserByEmailAsync(string email)
    {
        //Это временная проверка
        if (!string.IsNullOrEmpty(email))
        {
            try
            {
                var result = await _UserManager.FindByEmailAsync(email);
                if (result is not null)
                {
                    return Ok(result);
                }

                return BadRequest("Не удалось получить информации о пользователе");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Ошибка");
                return BadRequest("Произошла ошибка при получении пользователей");
            }
        }

        return BadRequest("Некорректно введены данные");
    }

    [Authorize(Roles = "admin")]
    [HttpPost("GetAllUsers")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        try
        {
            var list_of_all_users = await _UserManager.Users.ToListAsync();
            return Ok(list_of_all_users);
        }
        catch (Exception ex)
        {
            _Logger.LogError(ex, "Ошибка");
            return BadRequest("Произошла ошибка при получении пользователя");
        }
    }

    [Authorize(Roles = "admin")]
    [HttpPost("EditUserByEmail")]
    public async Task<IActionResult> EditUserByEmailAsync(string email, UserRequest EditUserRequest)
    {
        //Это временная проверка
        if (!string.IsNullOrEmpty(email) || EditUserRequest is not null)
        {
            try
            {
                var found_user = await _UserManager.FindByEmailAsync(email);
                if (found_user is not null)
                {
                    found_user.Email = EditUserRequest.Email;
                    found_user.PhoneNumber = EditUserRequest.PhoneNumber;
                    found_user.UserName = EditUserRequest.Email;

                    var result = await _UserManager.UpdateAsync(found_user);
                    if (result.Succeeded)
                    {
                        return Ok(result);
                    }

                    return BadRequest("Не удалось обновить информацию пользователя");
                }

                return BadRequest("Не удалось найти данного пользователя или некорректно введены данные пользователя");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Ошибка");
                return BadRequest("Произошла ошибка при обновлении информации о пользователе");
            }
        }

        return BadRequest("Некорректно введены данные пользователя");
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("DeleteUserByEmail")]
    public async Task<IActionResult> DeleteUserByEmail(string email)
    {
        //Это временная проверка
        if (!string.IsNullOrEmpty(email))
        {
            try
            {
                var found_user = await _UserManager.FindByEmailAsync(email);
                if (found_user is not null)
                {
                    var result = await _UserManager.DeleteAsync(found_user);
                    if (result.Succeeded)
                    {
                        return Ok(result);
                    }

                    return BadRequest("Не удалось удалить пользователя");
                }

                return BadRequest("Не удалось получить информацию о пользователе");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Ошибка");
                return BadRequest("Произошла ошибка при удалении пользователя");
            }
        }

        return BadRequest("Некорректно введены данные пользователя");
    }
}