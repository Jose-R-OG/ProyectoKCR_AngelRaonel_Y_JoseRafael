namespace KCR;

using KCR.Data;
using KCR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Threading.Tasks;

public static class IdentitySeeder
{
    public static readonly string AdminRole = "Administrador";

    public static readonly string EmpleadoExpressRole = "EmpleadoExpress";
    public static readonly string EmpleadoDERole = "EmpleadoDE";

    public static async Task SeedRolesAndAdminAsync(
        IServiceProvider serviceProvider,
        string adminPassword)
    {

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

        await CreateRoleIfNotExists(roleManager, AdminRole);
        await CreateRoleIfNotExists(roleManager, EmpleadoExpressRole);
        await CreateRoleIfNotExists(roleManager, EmpleadoDERole);


        await CreateAdminUser(userManager, dbContext, AdminRole, adminPassword);

        string passwordEmpleados = "PasswordSeguro123!";


        await CreateUserIfNotExists(
            userManager,
            dbContext,
            "EmpleadoExpress@kcr.com",
            passwordEmpleados,
            EmpleadoExpressRole,
            "Empleado Express",      
            "000-0000000-1",       
            "Operador Express"    
        );

        await CreateUserIfNotExists(
            userManager,
            dbContext,
            "EmpleadoDE@kcr.com",
            passwordEmpleados,
            EmpleadoDERole,
            "Empleado DE",          
            "000-0000000-2",       
            "Operador DE"           
        );
    }

    // Método auxiliar para crear roles
    private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    private static async Task CreateAdminUser(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context,
        string adminRole,
        string adminPassword)
    {
        const string adminEmail = "admin@kcr.com";

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                Nombre = "Administrador Principal",
                Cedula = "000-0000000-0",
                Cargo = "Administrador General"
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);

                var adminEmpleado = new Empleados
                {
                    Nombre = adminUser.Nombre,
                    Usuario = adminUser.Email!, 
                    Cedula = adminUser.Cedula,
                    Cargo = adminUser.Cargo,
                    Clave = "SEED_IDENTITY_MANAGED"
                };


                context.empleados.Add(adminEmpleado);
                await context.SaveChangesAsync();
  
            }
        }
    }


    private static async Task CreateUserIfNotExists(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context,
        string email,
        string password,
        string role,
        string nombre,
        string cedula,
        string cargo)
    {
        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                Nombre = nombre,
                Cedula = cedula,
                Cargo = cargo
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                var empleado = new Empleados
                {
                    Nombre = nombre,
                    Usuario = email, 
                    Cedula = cedula,
                    Cargo = cargo,
                    Clave = "SEED_IDENTITY_MANAGED"
                };


                context.empleados.Add(empleado);
                await context.SaveChangesAsync();

            }
        }
    }
}