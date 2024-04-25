#nowarn "20"

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Westwind.AspNetCore.LiveReload

open ShowAndTell.Components.ViewComponents

let builder = WebApplication.CreateBuilder()

builder.Services.AddLiveReload()

builder
    .Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation()

builder.Services.AddRazorPages()

builder.Services.AddSingleton<CartsRepository>()

let app = builder.Build()

if not (builder.Environment.IsDevelopment()) then
    app.UseExceptionHandler("/Home/Error")
    app.UseHsts() |> ignore // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

app.UseLiveReload()

app.UseStaticFiles()
app.UseRouting()
app.UseAuthorization()

app.MapControllerRoute(name = "default", pattern = "{controller=Home}/{action=Index}/{id?}")

app.MapRazorPages()

app.Run()
