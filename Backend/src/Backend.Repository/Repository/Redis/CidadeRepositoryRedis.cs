using Backend.Infra.Data.model;
using Serilog;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.Redis
{
    public interface ICidadeRepositoryRedis
    {
        public CidadeDto? GetId(int id);
        public bool Set(CidadeDto cidade);
        public bool Delete(int id);
    }
    public class CidadeRepositoryRedis : ICidadeRepositoryRedis
    {
        
        private readonly IConnectionMultiplexer _connection;
        public CidadeRepositoryRedis(IConnectionMultiplexer connection)
        {
            _connection = connection;
        }
        public CidadeDto? GetId(int id)
        {
            string templateLog = "[Backend.Api] [CidadeRepositoryRedis] [GetId]";
            Log.Information($"{templateLog} Iniciando GetId, Checando se existe uma cidade com o id passado");
            try{
                var client = _connection.GetDatabase();
                var result = client.HashGetAll(id + "cliente");
                if (result is not null)
                {
                    if (result.Length>0 && result[0].Name == "nome")
                    {
                        return new CidadeDto { id = id, nome = result[0].Value, uF = result[1].Value };
                    }
                    return new CidadeDto { id = id, nome = result[1].Value, uF = result[0].Value };
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }
        public bool Set(CidadeDto cidade)
        {
            string templateLog = "[Backend.Api] [CidadeRepositoryRedis] [GetId]";
            Log.Information($"{templateLog} Iniciando Set, Settando propriedade");
            var client = _connection.GetDatabase();
            client.HashSet(cidade.id + "cliente", new HashEntry[] { new HashEntry("nome", cidade.nome), new HashEntry("uF", cidade.uF) });
            return true;
        }
        public bool Delete(int id)
        {
            string templateLog = "[Backend.Api] [CidadeRepositoryRedis] [GetId]";
            Log.Information($"{templateLog} Iniciando Set, Settando propriedade");
            var client = _connection.GetDatabase();
            client.KeyDelete(id + "cliente");
            return true;
        }
    }
    internal class HashResult 
    {
        public string? nome { get; set; }
        public string? uF { get; set; }
    }
}
