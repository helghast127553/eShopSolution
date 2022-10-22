var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
<<<<<<< Updated upstream
=======
builder.Services.AddHttpClient();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(5);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
          {
              options.LoginPath = "/Account/Login";
              options.AccessDeniedPath = "/User/Forbidden/";
          });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUserApiClient, UserApiClient>();
builder.Services.AddTransient<ICategoryApiClient, CategoryApiClient>();
>>>>>>> Stashed changes

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
