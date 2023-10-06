using AccountingApp.Infrastructure.Data;
using AccountingApp.UseCases;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

string? connectionString = builder.Configuration.GetConnectionString("AccountingDbConnection");
builder.Services.AddDbContext<AccountingDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.ShortSchemaNames = true;
});

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacUseCasesModule());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseDefaultExceptionHandler();
    app.UseHsts();
}

app.UseFastEndpoints();
app.UseSwaggerGen();

app.UseHttpsRedirection();

app.Run();