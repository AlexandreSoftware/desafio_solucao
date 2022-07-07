using Backend.Infra.Data.Context;
using Backend.Infra.Data.model;
using Backend.Repository.EF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.EF
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly PessoasContext PessoasContext;
        public PessoaRepository(PessoasContext pessoasContext)
        {
            this.PessoasContext = pessoasContext;
        }
        public List<Pessoa> GetAll(int Page)
        {
            int take = 50;
            int skip = Page * take;
            var pessoas = PessoasContext.Pessoas.Skip(skip).Take(take);
            return pessoas.ToList();
        }
        public Pessoa GetId(int id)
        {
            var pessoa = PessoasContext.Pessoas.FirstOrDefault(x => x.id == id);
            if (pessoa is not null)
            {
                return pessoa;
            }
            throw new IOException("Nao conseguimos achar a pessoa");

        }
        public bool Post(Pessoa p)
        {
            var pessoa = PessoasContext.Pessoas.FirstOrDefault(x => x.id == p.id);
            var cidade = PessoasContext.Cidades.FirstOrDefault(x => x.id == p.cidade.id);
            if (pessoa is not null && cidade is not null)
            {
                PessoasContext.Pessoas.Update(p);
                PessoasContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public int Put(Pessoa p)
        {
            var pessoa = PessoasContext.Pessoas.FirstOrDefault(x => x.id == p.id);
            var cidade = PessoasContext.Cidades.FirstOrDefault(x => x.id == p.cidade.id);
            if (pessoa is null && cidade is not null && cidade.nome == p.cidade.nome && cidade.UF == p.cidade.UF)
            {
                var pessoaAdicionada = PessoasContext.Pessoas.Add(p);
                PessoasContext.SaveChanges();
                return p.id;
            }
            else
            {
                throw new IOException("Ja exite uma Pessoa");
            }

        }
        public bool Delete(int id)
        {
            var pessoa = PessoasContext.Pessoas.FirstOrDefault(x => x.id == id);
            if (pessoa is not null)
            {
                PessoasContext.Pessoas.Remove(pessoa);
                PessoasContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
