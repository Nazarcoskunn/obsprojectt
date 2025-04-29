using Microsoft.EntityFrameworkCore;
using ObsBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// PostgreSQL bağlantısını yapılandır
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Session ekliyoruz (kullanıcı bilgilerini tutabilmek için)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Sıralama önemli! UseRouting'ten önce değil, SONRA yapılmalı
app.UseRouting();

// Session ve Authentication middleware'lerini kullan
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Eğer hiç kullanıcı yoksa, bir admin ekle
    if (!dbContext.Users.Any())
    {
       // Kullanıcıyı ekliyoruz
dbContext.Users.Add(new ObsBackend.Model.User
{
    Email = "admin@abcuniversity.com",
    Password = "1234",
    Role = "student"
});

// Değişiklikleri kaydediyoruz
dbContext.SaveChanges();

    }
}


// Default Route: Başlangıç Auth/Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
