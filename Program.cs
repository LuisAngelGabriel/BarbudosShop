using BarbudosShop.Components;
using BarbudosShop.Components.Account;
using BarbudosShop.Data;
using BarbudosShop.Services;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddBlazoredToast();
builder.Services.AddRazorPages();

builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<CarritoService>();
builder.Services.AddScoped<SugerenciaService>();
builder.Services.AddScoped<BarbudosShop.Services.PedidoService>();

builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1024 * 300;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Admin", "Cliente" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var adminEmail = "admin@gmail.com";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, "Admin2025#");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        else
        {
            throw new Exception($"Error creando usuario Admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }

    var clienteEmail = "cliente@gmail.com";
    if (await userManager.FindByEmailAsync(clienteEmail) == null)
    {
        var clienteUser = new ApplicationUser
        {
            UserName = clienteEmail,
            Email = clienteEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(clienteUser, "Cliente123@");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(clienteUser, "Cliente");
        }
        else
        {
            throw new Exception($"Error creando usuario Cliente: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }

    var usuarios = userManager.Users.ToList();
    foreach (var usuario in usuarios)
    {
        if (!usuario.EmailConfirmed)
        {
            usuario.EmailConfirmed = true;
            var updateResult = await userManager.UpdateAsync(usuario);
            if (!updateResult.Succeeded)
            {
                throw new Exception($"Error confirmando email para usuario {usuario.Email}: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
            }
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapRazorPages();

app.Run();