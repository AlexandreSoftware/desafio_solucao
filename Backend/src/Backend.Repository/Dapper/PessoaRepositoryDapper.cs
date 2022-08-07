using Backend.Infra.Data.model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.Dapper;

public interface IPessoaRepositoryDapper
{
    public List<PessoaDto> GetAll(int Page);
    public PessoaDto GetId(int id);
    public bool Post(PessoaDto p);
    public int Put(PessoaDto p);
    public bool Delete(int id);
}
public class PessoaRepositoryDapper : IPessoaRepositoryDapper
{
    private readonly IDapperWrapper DW;
    public PessoaRepositoryDapper(IDapperWrapper dW)
    {
        this.DW = dW;
    }
    public List<PessoaDto> GetAll(int Page)
    {
        string templateLog = "[Backend.Api] [PessoaRepositoryDapper] [GetAll]";
        Log.Information($"{templateLog} Iniciando GetAll, calculando o take e o skip");
        int take = 50;
        int skip = Page * take;
        Log.Information($"{templateLog} take e skip calculados, iniciando a consulta");
        var pessoas = DW.QueryParams<PessoaDto>(
        "SELECT *"
        + "FROM [dbo].Pessoas"
        + "ORDER BY OrderDate"
        + "OFFSET(@Skip) ROWS FETCH NEXT(@Take) ROWS ONLY", new
        {
            Skip = skip,
            Take = take
        });
        if (pessoas is not null)
        {
            Log.Information($"{templateLog} consulta finalizada, retornando");
            return pessoas.ToList();
        }
        else
        {
            return new();
        }
    }
    public PessoaDto GetId(int id)
    {
        string templateLog = "[Backend.Api] [PessoaRepositoryDapper] [GetId]";
        Log.Information($"{templateLog} Iniciando GetId, Checando se existe uma pessoa com o id passado");


        var pessoa = DW.QueryParams<PessoaDto>(
        "SELECT *"
        + "FROM dbo.Pessoas"
        + "Where id= @Id", new
        {
            Id = id
        });
        if (pessoa is not null)
        {
            Log.Information($"{templateLog} Pessoa encontrada, retornando");
            return pessoa.ToArray()[0];
        }
        Log.Information($"{templateLog} Pessoa não encontrada, jogando excessao");
        throw new IOException("Nao conseguimos achar a pessoa");

    }
    public bool Post(PessoaDto p)
    {
        string templateLog = "[Backend.Api] [PessoaRepositoryDapper] [Post]";
        Log.Information($"{templateLog} Iniciando Post, checando se a pessoa existe");

        try
        {
            DW.ExecuteParams("Update dbo.Pessoas " +
                "SET nome = @Nome" +
                ",cpf= @Cpf" +
                ",idade=@Idade"+
                ",id_cidade = @Id_Cidade" + 
                "Where id= @ID; ", new { Nome = p.nome, Cpf= p.cpf, ID = p.id , Id_Cidade = p.id_cidade});
            return true;
        }
        catch (Exception e)
        {
            Log.Error("ERROR: nao e possivel atualizar a pessoa : " + e.Message);
            return false;
        }

    }
    public int Put(PessoaDto c)
    {
        string templateLog = "[Backend.Api] [PessoaRepositoryDapper] [Put]";
        Log.Information($"{templateLog} Iniciando Put, colocando o id como nulo e inserindo");
        try
        {
            var result = DW.QueryParams<int>("INSERT INTO dbo.Pessoas " +
                "Values (nome, cpf,idade, id_cidade" +
                "OUTPUT INSERTED.id" +
                "Values (@Nome, @Cpf,@Idade,@Id_Cidade)", new { Nome = c.nome, Cpf = c.cpf, ID = c.id, Id_Cidade = c.id_cidade });
            Log.Information($"{templateLog} Pessoa inserida, retornando");
            return result.ToArray()[0];
        }
        catch (Exception e)
        {
            Log.Error("ERROR: nao e possivel Criar a pessoa : " + e.Message);
            throw new IOException("ERROR: nao e possivel Criar a pessoa ");
        }
    }
    public bool Delete(int id)
    {
        string templateLog = "[Backend.Api] [PessoaRepositoryDapper] [Delete]";
        Log.Information($"{templateLog} Iniciando Delete, checando se existe uma pessoa com o id passado");
        var pessoa = DW.QueryParams<PessoaDto>(
           "SELECT *"
           + "FROM dbo.Pessoas"
           + "Where id= @Id", new
           {
               Id = id
           });
        if (pessoa is not null)
        {
            Log.Information($"{templateLog} Pessoa encontrada, removendo");
            DW.ExecuteParams(
               "DELETE"
               + "FROM dbo.Pessoas"
               + "Where id= @Id", new
               {
                   Id = id
               });
            Log.Information($"{templateLog} Pessoa removida, retornando");
            return true;
        }
        else
        {
            Log.Information($"{templateLog} Pessoa não encontrada, retornando falso");
            return false;
        }
    }
}
