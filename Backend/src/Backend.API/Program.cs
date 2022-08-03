using Backend.Infra.Data.Context;
using Backend.Repository;
using Backend.Repository.Context;
using Backend.Service.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//serilog
builder.Host.UseSerilog((ctx, lc) =>
lc
    .WriteTo.Console()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("SeqString"))
    .WriteTo.Elasticsearch(builder.Configuration.GetConnectionString("ElasticString"))
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    );
// swagger
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
//automapper
builder.Services.AddAutoMapper(typeof(PessoaProfile), typeof(CidadeProfile));
//dbconnection
builder.Services.AddDbContext<PessoasContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//redis
var redisConnection = builder.Configuration.GetConnectionString("RedisConnection");
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(
    redisConnection
    ));
//Pessoa 
builder.Services.AddScoped<Backend.Repository.EF.Interface.IPessoaRepository, Backend.Repository.EF.PessoaRepository>();
builder.Services.AddScoped<Backend.Service.EF.Interface.IPessoaService, Backend.Service.EF.PessoaService>();
//Cidade
builder.Services.AddScoped<Backend.Repository.EF.Interface.ICidadeRepository, Backend.Repository.EF.CidadeRepository>();
builder.Services.AddScoped<Backend.Repository.Redis.ICidadeRepositoryRedis, Backend.Repository.Redis.CidadeRepositoryRedis>();
builder.Services.AddScoped<Backend.Service.EF.Interface.ICidadeService, Backend.Service.EF.CidadeService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
//UseSwagger
app.UseSwagger();
app.UseSwaggerUI();
//context
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<PessoasContext>();
    dataContext.Database.Migrate();
    PessoaSeeder.Seed(dataContext);
}

app.UseAuthorization();

app.MapControllers();

app.Run();

