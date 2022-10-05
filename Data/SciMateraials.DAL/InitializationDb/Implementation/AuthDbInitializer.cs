using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace SciMaterials.DAL.InitializationDb.Implementation;

public static class AuthDbInitializer
{
    /// <summary>
    /// Инициализация базы данных с созданием ролей "супер админ" и "пользователь"
    /// Создание одной учетной записи "админа"
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="roleManager"></param>
    public static async Task InitializeAsync(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        string super_admin_email = configuration.GetSection("SuperAdminSettings:Login").Value;
        string super_admin_password = configuration.GetSection("SuperAdminSettings:Password").Value;
        
        //Роль админа
        if (await roleManager.FindByNameAsync("admin") is null)
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
        }

        //Роль пользователя
        if (await roleManager.FindByNameAsync("user") is null)
        {
            await roleManager.CreateAsync(new IdentityRole("user"));
        }
        
        //Супер админ
        if (await userManager.FindByNameAsync(super_admin_email) is null)
        {
            var super_admin = new IdentityUser
            {
                Email = super_admin_email, 
                UserName = super_admin_email
            };
            
            if (await userManager.CreateAsync(super_admin, super_admin_password) is { Succeeded: true })
            {
                await userManager.AddToRoleAsync(super_admin, "admin");
            }
        }
    }
}