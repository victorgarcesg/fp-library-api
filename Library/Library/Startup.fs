namespace Library

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Library.Services
open Library.Persistence
open Microsoft.EntityFrameworkCore
open Library.Repositories

type Startup private () =
    new (configuration: IConfiguration) as __ =
        Startup() then
        __.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member __.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddDbContext<LibraryContext> (fun (options : DbContextOptionsBuilder) -> 
        options.UseSqlServer("Server=database-library.c6v5baofubzt.us-east-1.rds.amazonaws.com,1521;Initial Catalog=Library;Persist Security Info=False;User ID=sa;Password=sa123456789;") 
        |> ignore) |> ignore
        
        services.AddScoped<IBooksService, BookService>() |> ignore

        services.AddScoped<BooksRepository, BooksRepository>() |> ignore

        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1) |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member __.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
        else
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts() |> ignore

        app.UseHttpsRedirection() |> ignore
        app.UseMvc() |> ignore

    member val Configuration : IConfiguration = null with get, set