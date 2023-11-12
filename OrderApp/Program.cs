using OrderApp.Database;
using OrderApp.Services;
using OrderApp.Services.Interfaces;
using OrderApp.Validators;
using OrderApp.Validators.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProviderService, ProviderService>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<IOrderValidator, OrderValidator>();
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Form}/{action=GetDataToShowMainPage}/{id?}");

app.Run();
