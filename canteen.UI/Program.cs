using canteen.Data;
using canteen.Data.DataAccess;
using canteen.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IcustomerRepository, customerRepository>();
builder.Services.AddTransient<IreceiptsRepository, receiptsRepository>();
builder.Services.AddTransient<IissueRepository, issueRepository>();




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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Add a route for the "Receipts" page
    endpoints.MapControllerRoute(
        name: "Receipts",
        pattern: "Customer/Receipts",
        defaults: new { controller = "Receipts", action = "Add" });
    
    endpoints.MapControllerRoute(
       name: "Issue",
       pattern: "Receipts/Issue",
       defaults: new { controller = "Issue", action = "Add" });





    // Adjust the controller and action names as needed

    // You can add additional endpoints or middleware here if needed

    // For example, if you want to add a Swagger UI for API documentation:
    // endpoints.MapSwagger();
});

app.Run();
