using Backend.Infra.Data.Context;
using Backend.Infra.Data.model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.EF
{
    public interface IPessoaRepositoryEF
    {
        public List<PessoaDto> GetAll(int Page);
        public PessoaDto GetId(int id);
        public bool Post(PessoaDto p);
        public int Put(PessoaDto p);
        public bool Delete(int id);
    }
    public class PessoaRepositoryEF : IPessoaRepositoryEF
    {
        private readonly PessoasContext PessoasContext;
        public PessoaRepositoryEF(PessoasContext pessoasContext)
        {
            this.PessoasContext = pessoasContext;
        }
        public List<PessoaDto> GetAll(int Page)
        {
            string templateLog = "[Backend.Api] [PessoaRepository] [GetAll]";
            Log.Information($"{templateLog} Iniciando GetAll, calculando o take e o skip");
            int take = 50;
            int skip = Page * take;
            Log.Information($"{templateLog} take e skip calculados, iniciando a consulta");
            var pessoas = PessoasContext.Pessoas.Skip(skip).Take(take);
            Log.Information($"{templateLog} consulta finalizada, retornando");
            return pessoas.ToList();
        }
        public PessoaDto GetId(int id)
        {
            string templateLog = "[Backend.Api] [PessoaRepository] [GetId]";
            Log.Information($"{templateLog} Iniciando GetId, Checando se existe uma pessoacom o id passado");
            var pessoa = PessoasContext.Pessoas.FirstOrDefault(x => x.id == id);
            if (pessoa is not null)
            {
                Log.Information($"{templateLog} Pessoa encontrada, retornando");
                return pessoa;
            }
            Log.Information($"{templateLog} Pessoa não encontrada, jogando excessao");
            throw new IOException("Nao conseguimos achar a pessoa");

        }
        public bool Post(PessoaDto p)
        {
            string templateLog = "[Backend.Api] [PessoaRepository] [Post]";
            Log.Information("{templateLog} Iniciando Post, checando se a cidade e pessoa existe");
            var pessoa = PessoasContext.Pessoas.FirstOrDefault(x => x.id == p.id);
            var cidade = PessoasContext.Cidades.FirstOrDefault(x => x.id == p.id_cidade);
            if (pessoa is not null && cidade is not null)
            {
                Log.Information($"{templateLog} Cidade e Pessoa ja existe, atualizando ela");
                pessoa.id = p.id;
                pessoa.idade = p.idade;
                pessoa.nome = p.nome;
                pessoa.id_cidade = p.id_cidade;
                pessoa.cpf = p.cpf;
                PessoasContext.SaveChanges();
                Log.Information($"{templateLog} Cidade atualizada, retornando");
                return true;
            }
            else
            {
                Log.Information($"{templateLog} Cidade ou Pessoa não existe, retornando false");
                return false;
            }

        }
        public int Put(PessoaDto p)
        {
            string templateLog = "[Backend.Api] [PessoaRepository] [Put]";
            Log.Information($"{templateLog} Iniciando Put, checando se existe uma cidade com o id passado");
            var cidade = PessoasContext.Cidades.FirstOrDefault(x => x.id == p.id_cidade);
            if (cidade is not null)
            {
                Log.Information($"{templateLog} Cidade existe, inserindo Pessoa");
                p.id = null;
                var pessoaAdicionada = PessoasContext.Pessoas.Add(p);
                PessoasContext.SaveChanges();
                Log.Information($"{templateLog} Pessoa inserida, retornando o id");
                if(p.id is not null)
                {
                    return (int)p.id;
                }
                throw new Exception("somehow p.id is null");
            }
            else
            {
                Log.Information($"{templateLog} Cidade não existe, jogando erro");
                throw new IOException("Cidade nao existe");
            }

        }
        public bool Delete(int id)
        {
            string templateLog = "[Backend.Api] [PessoaRepository] [Delete]";
            Log.Information($"{templateLog} Iniciando Delete, checando se existe uma pessoa com o id passado");
            var pessoa = PessoasContext.Pessoas.FirstOrDefault(x => x.id == id);
            if (pessoa is not null)
            {
                Log.Information($"{templateLog} Pessoa existe, deletando");
                PessoasContext.Pessoas.Remove(pessoa);
                PessoasContext.SaveChanges();
                Log.Information($"{templateLog} Pessoa deletada, retornando true");
                return true;
            }
            else
            {
                Log.Information($"{templateLog} Pessoa não existe, retornando false");
                return false;
            }

        }

    }
}
