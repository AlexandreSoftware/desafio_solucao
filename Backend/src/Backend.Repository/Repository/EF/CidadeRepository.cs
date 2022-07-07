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
    public class CidadeRepository : ICidadeRepository
    {
        private readonly PessoasContext PessoasContext;
        public CidadeRepository(PessoasContext CidadesContext)
        {
            this.PessoasContext = CidadesContext;
        }
        public List<Cidade> GetAll(int Page)
        {
            int take = 50;
            int skip = Page * take;
            var cidades = PessoasContext.Cidades.Skip(skip).Take(take);
            return cidades.ToList();


        }
        public Cidade GetId(int id)
        {
            var cidade = PessoasContext.Cidades.FirstOrDefault(x => x.id == id);
            if (cidade is not null)
            {
                return cidade;
            }
            throw new IOException("Nao conseguimos achar a cidade");

        }
        public bool Post(Cidade p)
        {
            var cidade = PessoasContext.Cidades.FirstOrDefault(x => x.id == p.id);
            if (cidade is not null)
            {
                PessoasContext.Cidades.Update(p);
                PessoasContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public int Put(Cidade p)
        {
            PessoasContext.Cidades.Add(p);
            PessoasContext.SaveChanges();
            return p.id;

        }
        public bool Delete(int id)
        {
            var Cidade = PessoasContext.Cidades.FirstOrDefault(x => x.id == id);
            if (Cidade is not null)
            {
                PessoasContext.Cidades.Remove(Cidade);
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
