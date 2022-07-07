using Backend.Infra.Data.Context;
using Backend.Repository;
using Backend.Repository.Context;
using Backend.Service.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//serilog
builder.Host.UseSerilog((ctx, lc) =>
lc
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:9240")
    .WriteTo.Elasticsearch("http://localhost:9200")
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(PessoaProfile), typeof(CidadeProfile));
builder.Services.AddDbContext<PessoasContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<Backend.Repository.EF.Interface.IPessoaRepository, Backend.Repository.EF.PessoaRepository>();
builder.Services.AddScoped<Backend.Service.EF.Interface.IPessoaService, Backend.Service.EF.PessoaService>();
builder.Services.AddScoped<Backend.Repository.EF.Interface.ICidadeRepository, Backend.Repository.EF.CidadeRepository>();
builder.Services.AddScoped<Backend.Service.EF.Interface.ICidadeService, Backend.Service.EF.CidadeService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<PessoasContext>();
    dataContext.Database.Migrate();
    PessoaSeeder.Seed(dataContext);
}

app.UseAuthorization();

app.MapControllers();

app.Run();

