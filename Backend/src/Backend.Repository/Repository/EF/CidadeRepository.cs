using Backend.Infra.Data.Context;
using Backend.Infra.Data.model;
using Backend.Repository.EF.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.EF
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly PessoasContext PessoasContext;
        public CidadeRepository(PessoasContext CidadesContext)
        {
            this.PessoasContext = CidadesContext;
        }
        public List<CidadeDto> GetAll(int Page)
        {
            string templateLog = "[Backend.Api] [CidadeRepository] [GetAll]";
            Log.Information($"{templateLog} Iniciando GetAll, calculando o take e o skip");
            int take = 50;
            int skip = Page * take;
            Log.Information($"{templateLog} take e skip calculados, iniciando a consulta");
            var cidades = PessoasContext.Cidades.Skip(skip).Take(take);
            Log.Information($"{templateLog} consulta finalizada, retornando");
            return cidades.ToList();


        }
        public CidadeDto GetId(int id)
        {
            string templateLog = "[Backend.Api] [CidadeRepository] [GetId]";
            Log.Information($"{templateLog} Iniciando GetId, Checando se existe uma cidade com o id passado");
            var cidade = PessoasContext.Cidades.FirstOrDefault(x => x.id == id);
            if (cidade is not null)
            {
                Log.Information($"{templateLog} Cidade encontrada, retornando");
                return cidade;
            }
            Log.Information($"{templateLog} Cidade não encontrada, jogando excessao");
            throw new IOException("Nao conseguimos achar a cidade");

        }
        public bool Post(CidadeDto c)
        {
            string templateLog = "[Backend.Api] [CidadeRepository] [Post]";
            Log.Information("{templateLog} Iniciando Post, checando se a cidade existe");
            var cidade = PessoasContext.Cidades.FirstOrDefault(x => x.id == c.id);
            if (cidade is not null)
            {
                Log.Information($"{templateLog} Cidade ja existe, atualizando ela");
                cidade.id=c.id;
                cidade.uF = c.uF;
                cidade.nome = c.nome;
                PessoasContext.SaveChanges();
                Log.Information($"{templateLog} Cidade atualizada, retornando");
                return true;
            }
            else
            {
                Log.Information($"{templateLog} Cidade não existe, retornando");
                return false;
            }

        }
        public int Put(CidadeDto c)
        {
            string templateLog = "[Backend.Api] [CidadeRepository] [Put]";
            Log.Information($"{templateLog} Iniciando Put, colocando o id como nulo e inserindo");
            c.id = null;
            PessoasContext.Cidades.Add(c);
            PessoasContext.SaveChanges();
            Log.Information($"{templateLog} Cidade inserida, retornando");
            return (int)c.id;

        }
        public bool Delete(int id)
        {
            string templateLog = "[Backend.Api] [CidadeRepository] [Delete]";
            Log.Information($"{templateLog} Iniciando Delete, checando se existe uma cidade com o id passado");
            var Cidade = PessoasContext.Cidades.FirstOrDefault(x => x.id == id);
            if (Cidade is not null)
            {
                Log.Information($"{templateLog} Cidade encontrada, removendo");
                PessoasContext.Cidades.Remove(Cidade);
                PessoasContext.SaveChanges();
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

}
