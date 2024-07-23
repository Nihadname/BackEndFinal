using BackEndFinal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;
// Add services to the container
builder.Services.Register(config);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=DashBoasrd}/{action=Index}/{id?}"
         );
app.MapDefaultControllerRoute();

app.Run();
