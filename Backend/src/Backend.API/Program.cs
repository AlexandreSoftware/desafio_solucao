using Backend.Infra.Data.Context;
using Backend.Repository;
using Backend.Repository.Context;
using Backend.Repository.Dapper;
using Backend.Repository.EF;
using Backend.Repository.Redis;
using Backend.Service.Dapper;
using Backend.Service.EF;
using Backend.Service.model;
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
builder.Services.AddTransient<IDapperWrapper, DapperWrapper>(x => new DapperWrapper(builder.Configuration.GetValue<string>("DefaultConnection")));

//redis
var redisConnection = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("RedisConnection"));
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(
    redisConnection
    ));
//Pessoa 
builder.Services.AddScoped<IPessoaRepositoryEF, PessoaRepositoryEF>();
builder.Services.AddScoped<IPessoaServiceEF, PessoaServiceEF>();
builder.Services.AddScoped<IPessoaRepositoryDapper, PessoaRepositoryDapper>();
builder.Services.AddScoped<IPessoaServiceDapper, PessoaServiceDapper>();
//Cidade
builder.Services.AddScoped<ICidadeRepositoryEF, CidadeRepositoryEF>();
builder.Services.AddScoped<ICidadeServiceEF, CidadeServiceEF>();
builder.Services.AddScoped<ICidadeRepositoryDapper, CidadeRepositoryDapper>();
builder.Services.AddScoped<ICidadeServiceDapper, CidadeServiceDapper>();
builder.Services.AddScoped<ICidadeRepositoryRedis, CidadeRepositoryRedis>();
var app = builder.Build();
// Configure the HTTP request pipeline.
//UseSwagger
app.UseSwagger();
app.UseSwaggerUI();
//context
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<PessoasContext>();
    //restaurando o banco de dados pra um estado inicial
    dataContext.Database.ExecuteSqlRaw("DELETE FROM [dbo].Cidades;DBCC CHECKIDENT ('[dbo].Cidades', RESEED, 1);");

    dataContext.Database.ExecuteSqlRaw("DELETE FROM [dbo].Pessoas;DBCC CHECKIDENT ('[dbo].Pessoas', RESEED, 1);");
    dataContext.Database.Migrate();   
    PessoaSeeder.Seed(dataContext);
}

app.UseAuthorization();

app.MapControllers();

app.Run();

