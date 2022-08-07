using Backend.Infra.Data.Context;
using Backend.Infra.Data.model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.Dapper;

public interface ICidadeRepositoryDapper
{
    public List<CidadeDto> GetAll(int Page);
    public CidadeDto GetId(int id);
    public bool Post(CidadeDto c);
    public int Put(CidadeDto c);
    public bool Delete(int id);
}
public class CidadeRepositoryDapper : ICidadeRepositoryDapper
{
    private readonly IDapperWrapper DW;
    public CidadeRepositoryDapper(IDapperWrapper dW)
    {
        this.DW = dW;
    }
    public List<CidadeDto> GetAll(int Page)
    {
        string templateLog = "[Backend.Api] [CidadeRepositoryDapper] [GetAll]";
        Log.Information($"{templateLog} Iniciando GetAll, calculando o take e o skip");
        int take = 50;
        int skip = Page * take;
        Log.Information($"{templateLog} take e skip calculados, iniciando a consulta");
        var cidades = DW.QueryParams<CidadeDto>(
        "SELECT *"
        + "FROM [dbo].Cidades"
        + "ORDER BY OrderDate"
        + "OFFSET(@Skip) ROWS FETCH NEXT(@Take) ROWS ONLY", new {
            Skip = skip,
            Take = take
        });
        if (cidades is not null)
        {
            Log.Information($"{templateLog} consulta finalizada, retornando");
            return cidades.ToList();
        }
        else
        {
            return new();
        }
    }
    public CidadeDto GetId(int id)
    {
        string templateLog = "[Backend.Api] [CidadeRepositoryDapper] [GetId]";
        Log.Information($"{templateLog} Iniciando GetId, Checando se existe uma cidade com o id passado");


        var cidade = DW.QueryParams<CidadeDto>(
        "SELECT *"
        + "FROM dbo.Cidades"
        + "Where id= @Id", new
        {
            Id = id
        });
        if (cidade is not null)
        {
            Log.Information($"{templateLog} Cidade encontrada, retornando");
            return cidade.ToArray()[0];
        }
        Log.Information($"{templateLog} Cidade não encontrada, jogando excessao");
        throw new IOException("Nao conseguimos achar a cidade");

    }
    public bool Post(CidadeDto c)
    {
        string templateLog = "[Backend.Api] [CidadeRepositoryDapper] [Post]";
        Log.Information($"{templateLog} Iniciando Post, checando se a cidade existe");

        try
        {
            DW.ExecuteParams("Update dbo.Cidades " +
                "SET nome = @Nome" +
                ",uF= @UF " +
                "Where id= @ID; ", new { Nome = c.nome, UF = c.uF, ID = c.id });
            return true;
        }
        catch (Exception e)
        {
            Log.Error("ERROR: nao e possivel atualizar a cidade : " + e.Message);
            return false;
        }

    }
    public int Put(CidadeDto c)
    {
        string templateLog = "[Backend.Api] [CidadeRepositoryDapper] [Put]";
        Log.Information($"{templateLog} Iniciando Put, colocando o id como nulo e inserindo");
        try
        {
            var result = DW.QueryParams<int>("INSERT INTO dbo.Cidades " +
                "Values (nome,uF)" +
                "OUTPUT INSERTED.id" +
                "Values (@Nome, @Uf)", new { Nome = c.nome, UF = c.uF });
            Log.Information($"{templateLog} Cidade inserida, retornando");
            return (int)result.ToArray()[0];
        }
        catch (Exception e)
        {
            Log.Error("ERROR: nao e possivel Criar a cidade : " + e.Message);
            throw new IOException("ERROR: nao e possivel Criar a cidade ");
        }

    }
    public bool Delete(int id)
    {
        string templateLog = "[Backend.Api] [CidadeRepositoryDapper] [Delete]";
        Log.Information($"{templateLog} Iniciando Delete, checando se existe uma cidade com o id passado");
        var cidade = DW.QueryParams<CidadeDto>(
           "SELECT *"
           + "FROM dbo.Cidades"
           + "Where id= @Id", new
           {
               Id = id
           });
        if (cidade is not null)
        {
            Log.Information($"{templateLog} Cidade encontrada, removendo");
            DW.ExecuteParams(
               "DELETE"
               + "FROM dbo.Cidades"
               + "Where id= @Id", new
               {
                   Id = id
               });
            Log.Information($"{templateLog} Cidade removida, retornando");
            return true;
        }
        else
        {
            Log.Information($"{templateLog} Cidade não encontrada, retornando falso");
            return false;
        }
    }
}
